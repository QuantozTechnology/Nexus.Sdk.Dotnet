using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nexus.SDK.Shared.Authentication;
using Nexus.SDK.Shared.Responses;
using Nexus.Token.Algorand.Examples.Models;
using Nexus.Token.Examples.SDK;
using Nexus.Token.SDK.Extensions;
using Serilog;

namespace Nexus.Token.Algorand.Examples
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            WriteToConsole("Welcome to the Stellar Examples project!");
            var provider = ConfigureServices();
            var algorandExample = provider.GetRequiredService<AlgorandExamples>();

            while (true)
            {
                WriteToConsole("Supported Flows:");
                WriteToConsole("0 = Algorand Payment Flow");
                WriteToConsole("1 = Algorand Payout Flow");
                WriteToConsole("2 = Algorand Token Taxonomy Flow");

                Console.Write("Please type in a number: ");
                var command = Console.ReadLine();

                if (command == "exit")
                {
                    break;
                }

                try
                {
                    switch (Convert.ToInt32(command))
                    {
                        case 0:
                            await AlgorandPaymentFlow(algorandExample);
                            break;
                        case 1:
                            await AlgorandPayoutFlow(algorandExample);
                            break;
                        case 2:
                            await AlgorandTaxonomyFlow(algorandExample);
                            break;
                        default:
                            WriteToConsole("Flow not supported");
                            break;
                    }
                }
                catch (OverflowException)
                {
                    WriteToConsole($"{command} is outside the range of the Int32 type.", ConsoleColor.Red);
                }
                catch (FormatException)
                {
                    WriteToConsole($"The {command.GetType().Name} value '{command}' is not in a recognizable format.", ConsoleColor.Red);
                }
                catch (NexusApiException ex)
                {
                    WriteToConsole($"{ex.StatusCode}", ConsoleColor.Red);
                    WriteToConsole($"{ex.Message}", ConsoleColor.Red);
                    WriteToConsole($"{ex.ErrorCodes}", ConsoleColor.Red);
                }
                catch (AuthProviderException ex)
                {
                    WriteToConsole($"{ex.Message}", ConsoleColor.Red);
                }
                catch (Exception ex)
                {
                    WriteToConsole(ex.Message, ConsoleColor.Red);
                }

                WriteToConsole("-----------------", ConsoleColor.White);
            }
        }

        private static ServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            Settings settings = config.GetRequiredSection("Settings").Get<Settings>();

            var logger = new LoggerConfiguration()
                 .MinimumLevel.Warning()
                 .WriteTo.Console()
                 .CreateLogger();

            services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(logger, dispose: true));

            services.AddTokenServer(o =>
                o.ConnectToCustom(settings.NexusApiUrl, settings.NexusIdentityUrl, settings.ClientId, settings.ClientSecret)
                 .AddDefaultFundingPaymentMethod(settings.FundingPaymentMethod)
                 .AddDefaultPayoutPaymentMethod(settings.PayoutPaymentMethod));

            services.UseSymmetricEncryption("b14ca5898a4e4133bbce2ea2315a1916");

            services.AddSingleton<AlgorandExamples>();

            return services.BuildServiceProvider();

        }

        public static async Task AlgorandPaymentFlow(AlgorandExamples algorandExample)
        {
            WriteToConsole("Create a new account for Bob");
            var bob = Guid.NewGuid().ToString();
            var bobsPrivateKey = await algorandExample.CreateAccountAsync(bob);

            WriteToConsole("Create a new account for Alice");
            var alice = Guid.NewGuid().ToString();
            var alicesPrivateKey = await algorandExample.CreateAccountAsync(alice);

            WriteToConsole("Create a new token representing the Mona Lisa");
            var tokenCode = Guid.NewGuid().ToString().Substring(0, 8);
            await algorandExample.CreateAssetTokenAsync(tokenCode, "Mona Lisa");

            WriteToConsole("Now we Fund Bobs new account with 100 tokens");
            await algorandExample.FundAccountAsync(bobsPrivateKey, tokenCode, 100);

            WriteToConsole("Then Bob sends 10 tokens to Alice.");
            await algorandExample.PaymentAsync(bobsPrivateKey, alicesPrivateKey, tokenCode, 10);
        }

        public static async Task AlgorandPayoutFlow(AlgorandExamples algorandExample)
        {
            WriteToConsole("Create a new account for Bob");
            var bob = Guid.NewGuid().ToString();
            var bobsPrivateKey = await algorandExample.CreateAccountAsync(bob);

            WriteToConsole("Create a new token representing the Mona Lisa");
            var tokenCode = Guid.NewGuid().ToString().Substring(0, 8);
            await algorandExample.CreateAssetTokenAsync(tokenCode, "Mona Lisa");

            WriteToConsole("Now we Fund Bobs new account with 100 tokens");
            await algorandExample.FundAccountAsync(bobsPrivateKey, tokenCode, 100);

            WriteToConsole("Bob waited for the tokens to increase in value and would like to get paid out now");
            await algorandExample.PayoutAsync(bobsPrivateKey, tokenCode, 10);
        }

        public static async Task AlgorandTaxonomyFlow(AlgorandExamples algorandExample)
        {
            WriteToConsole("Create a new token that represents Bob and has the taxonomy to prove it");
            var tokenCode = Guid.NewGuid().ToString().Substring(0, 8);
            await algorandExample.CreateAssetTokenWithTaxonomyAsync(tokenCode, "BOB");
        }

        private static void WriteToConsole(string message, ConsoleColor textColor = ConsoleColor.Green)
        {
            Console.ForegroundColor = textColor;
            WriteToConsole(message);
        }
    }
}