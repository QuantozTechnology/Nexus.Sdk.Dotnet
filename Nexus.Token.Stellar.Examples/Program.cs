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
using Nexus.Token.SDK.Requests;
using Nexus.Token.Stellar.Examples.Models;
using Serilog;

namespace Nexus.Token.Stellar.Examples
{
    public class Program
    {
        public static async Task Main(string[] args)
        {

            WriteToConsole("Welcome to the Stellar Examples project!");

            var provider = ConfigureServices();
            var stellarExamples = provider.GetRequiredService<StellarExamples>();

            while (true)
            {
                WriteToConsole("Supported Flows:");
                WriteToConsole("0 = Stellar Payment Flow");
                WriteToConsole("1 = Stellar Payout Flow");
                WriteToConsole("2 = Stellar Token Taxonomy Flow");
                WriteToConsole("3 = Stellar Token Orderbook Flow");
                WriteToConsole("4 = Stellar Stablecoin Flow");

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
                            await StellarPaymentFlow(stellarExamples);
                            break;
                        case 1:
                            await StellarPayoutFlow(stellarExamples);
                            break;
                        case 2:
                            await StellarTaxonomyFlow(stellarExamples);
                            break;
                        case 3:
                            await StellarOrderbookFlow(stellarExamples);
                            break;
                        case 4:
                            await StellarStablecoinFlow(stellarExamples);
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

            services.AddSingleton(settings);

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

            services.AddSingleton<StellarExamples>();

            return services.BuildServiceProvider();

        }

        public static async Task StellarPaymentFlow(StellarExamples stellarExamples)
        {
            WriteToConsole("Create a new account for Bob");
            var bob = Guid.NewGuid().ToString();
            var bobsPrivateKey = await stellarExamples.CreateAccountAsync(bob);

            WriteToConsole("Create a new account for Alice");
            var alice = Guid.NewGuid().ToString();
            var alicesPrivateKey = await stellarExamples.CreateAccountAsync(alice);

            WriteToConsole("Create a new token representing the Mona Lisa");
            var tokenCode = Guid.NewGuid().ToString().Substring(0, 8);
            await stellarExamples.CreateAssetTokenAsync(tokenCode, "Mona Lisa");

            WriteToConsole("Now we Fund Bobs new account with 100 tokens");
            await stellarExamples.FundAccountAsync(bobsPrivateKey, tokenCode, 100);

            WriteToConsole("Then Bob sends 10 tokens to Alice.");
            await stellarExamples.PaymentAsync(bobsPrivateKey, alicesPrivateKey, tokenCode, 10);
        }

        public static async Task StellarPayoutFlow(StellarExamples stellarExamples)
        {
            WriteToConsole("Create a new account for Bob");
            var bob = Guid.NewGuid().ToString();
            var bobsPrivateKey = await stellarExamples.CreateAccountAsync(bob);

            WriteToConsole("Create a new token representing the Mona Lisa");
            var tokenCode = Guid.NewGuid().ToString().Substring(0, 8);
            await stellarExamples.CreateAssetTokenAsync(tokenCode, "Mona Lisa");

            WriteToConsole("Now we Fund Bobs new account with 100 tokens");
            await stellarExamples.FundAccountAsync(bobsPrivateKey, tokenCode, 100);

            WriteToConsole("Bob waited for the tokens to increase in value and would like to get paid out now");
            await stellarExamples.PayoutAsync(bobsPrivateKey, tokenCode, 10);
        }

        public static async Task StellarTaxonomyFlow(StellarExamples stellarExamples)
        {
            WriteToConsole("Create a new token that represents Bob and has the taxonomy to prove it");
            var tokenCode = Guid.NewGuid().ToString().Substring(0, 8);
            await stellarExamples.CreateAssetTokenWithTaxonomyAsync(tokenCode, "BOB");
        }

        public static async Task StellarStablecoinFlow(StellarExamples stellarExamples)
        {
            WriteToConsole("Create a new token pegged to the Euro (Stablecoin)");
            var stablecoinTokenCode = Guid.NewGuid().ToString().Substring(0, 8);
            await stellarExamples.CreateStableCoinTokenAsync(stablecoinTokenCode, "EURO", "EUR", 1);

            WriteToConsole("Create a new token representing a share in the Mona Lisa");
            var assetTokenCode = Guid.NewGuid().ToString().Substring(0, 8);
            await stellarExamples.CreateAssetTokenAsync(assetTokenCode, "Mona Lisa");

            WriteToConsole("Create a new account for Bob");
            var bob = Guid.NewGuid().ToString();
            var bobsPrivateKey = await stellarExamples.CreateAccountAsync(bob, new string[] { assetTokenCode, stablecoinTokenCode });

            WriteToConsole("Now we Fund Bob's new account with 200 stablecoins");
            await stellarExamples.FundAccountAsync(bobsPrivateKey, stablecoinTokenCode, 200);

            WriteToConsole("Create a new account to hold the Mona Lisa asset");
            var assetHolder = Guid.NewGuid().ToString();
            var assetHoldersPrivateKey = await stellarExamples.CreateAccountAsync(assetHolder, new string[] { assetTokenCode, stablecoinTokenCode });

            WriteToConsole("Now we Fund the Asset Holders new account with a large amount of shares");
            await stellarExamples.FundAccountAsync(assetHoldersPrivateKey, assetTokenCode, 100000);

            WriteToConsole("Finally we create a two way atomic payment where 100 shares are exchanges for 100 euro.");
            var payments = new ExamplePayment[]
            {
                new ExamplePayment(bobsPrivateKey, assetHoldersPrivateKey, stablecoinTokenCode, 100),
                new ExamplePayment(assetHoldersPrivateKey, bobsPrivateKey, assetTokenCode, 100)
            };
            await stellarExamples.MultiplePaymentsAsync(payments);
        }

        public static async Task StellarOrderbookFlow(StellarExamples stellarExamples)
        {
            WriteToConsole("Create a new account for Bob");
            var bob = Guid.NewGuid().ToString();
            var bobsPrivateKey = await stellarExamples.CreateAccountAsync(bob);

            WriteToConsole("Create a new account for Alice");
            var alice = Guid.NewGuid().ToString();
            var alicesPrivateKey = await stellarExamples.CreateAccountAsync(alice);

            WriteToConsole("Create a new token representing the Mona Lisa (Asset)");
            var assetTokenCode = Guid.NewGuid().ToString().Substring(0, 8);
            await stellarExamples.CreateAssetTokenAsync(assetTokenCode, "Mona Lisa");

            WriteToConsole("Create a new token pegged to the Euro (Stablecoin)");
            var stablecoinTokenCode = Guid.NewGuid().ToString().Substring(0, 8);
            await stellarExamples.CreateStableCoinTokenAsync(stablecoinTokenCode, "EURO", "EUR", 1);

            WriteToConsole("Now we Fund Bob's new account with 100 asset tokens");
            await stellarExamples.FundAccountAsync(bobsPrivateKey, assetTokenCode, 100);

            WriteToConsole("And then Fund Alice's new account with 100 stable coin tokens");
            await stellarExamples.FundAccountAsync(alicesPrivateKey, stablecoinTokenCode, 100);

            WriteToConsole("Bob places a Sell order to sell 50 asset tokens for 50 Euro.");
            await stellarExamples.CreateSellOrder(bobsPrivateKey, (assetTokenCode, 50), (stablecoinTokenCode, 50));

            WriteToConsole("Somewhere else in the world, Alice would like to purchase 50 asset tokens for 50 Euros.");
            await stellarExamples.CreateBuyOrder(alicesPrivateKey, (assetTokenCode, 50), (stablecoinTokenCode, 50));
        }

        private static void WriteToConsole(string message, ConsoleColor textColor = ConsoleColor.Green)
        {
            Console.ForegroundColor = textColor;
            Console.WriteLine(message);
        }
    }
}