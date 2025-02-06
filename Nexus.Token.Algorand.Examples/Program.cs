using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nexus.Sdk.Shared.ErrorHandling;
using Nexus.Sdk.Token.Extensions;
using Serilog;

namespace Nexus.Token.Algorand.Examples
{
    public class Program
    {
        public static async Task Main()
        {
            WriteToConsole("Welcome to the Algorand Examples project!");
            var provider = ConfigureServices();
            var algorandExample = provider.GetRequiredService<AlgorandExamples>();

            while (true)
            {
                WriteToConsole("Supported Flows:");
                WriteToConsole("0 = Algorand Payment Flow");
                WriteToConsole("1 = Algorand Payout Flow");
                WriteToConsole("2 = Algorand Token Taxonomy Flow");
                WriteToConsole("3 = Algorand Multiple Operations Flow");
                WriteToConsole("4 = Algorand Token Limits Flow");
                WriteToConsole("5 = Algorand Update Token Operation Status Flow");

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
                        case 3:
                            await AlgorandMultipleOperationsFlow(algorandExample);
                            break;
                        case 4:
                            await AlgorandTokenLimitsFlow(algorandExample);
                            break;
                        case 5:
                            await AlgorandUpdateTokenOperationStatusFlow(algorandExample);
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
                    WriteToConsole($"The {command?.GetType().Name} value '{command}' is not in a recognizable format.", ConsoleColor.Red);
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

            var logger = new LoggerConfiguration()
                 .WriteTo.Console()
                 .CreateLogger();

            services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(logger, dispose: true));

            services.AddTokenServer(config);

            services.UseSymmetricEncryption("b14ca5898a4e4133bbce2ea2315a1916");

            services.AddSingleton<AlgorandExamples>();

            return services.BuildServiceProvider();

        }

        public static async Task AlgorandPaymentFlow(AlgorandExamples algorandExamples)
        {
            WriteToConsole("Create a new account for Bob");
            var bob = Guid.NewGuid().ToString();
            var bobsPrivateKey = await algorandExamples.CreateAccountAsync(bob);

            WriteToConsole("Create a new account for Alice");
            var alice = Guid.NewGuid().ToString();
            var alicesPrivateKey = await algorandExamples.CreateAccountAsync(alice);

            WriteToConsole("Create a new token representing the Mona Lisa");
            var tokenCode = Guid.NewGuid().ToString()[..8];
            await algorandExamples.CreateAssetTokenAsync(tokenCode, "Mona Lisa");

            WriteToConsole("Now we Fund Bobs new account with 100 tokens");
            await algorandExamples.FundAccountAsync(bobsPrivateKey, tokenCode, 100);

            WriteToConsole("Then Bob sends 10 tokens to Alice.");
            await algorandExamples.PaymentAsync(bobsPrivateKey, alicesPrivateKey, tokenCode, 10);
        }

        public static async Task AlgorandPayoutFlow(AlgorandExamples algorandExamples)
        {
            WriteToConsole("Create a new account for Bob");
            var bob = Guid.NewGuid().ToString();
            var bobsPrivateKey = await algorandExamples.CreateAccountAsync(bob);

            WriteToConsole("Create a new token representing the Mona Lisa");
            var tokenCode = Guid.NewGuid().ToString()[..8];
            await algorandExamples.CreateAssetTokenAsync(tokenCode, "Mona Lisa");

            WriteToConsole("Now we Fund Bobs new account with 100 tokens");
            await algorandExamples.FundAccountAsync(bobsPrivateKey, tokenCode, 100);

            WriteToConsole("Bob waited for the tokens to increase in value and would like to get paid out now");
            await algorandExamples.PayoutAsync(bobsPrivateKey, tokenCode, 10);
        }

        public static async Task AlgorandTaxonomyFlow(AlgorandExamples algorandExamples)
        {
            WriteToConsole("Create a new token that represents Bob and has the taxonomy to prove it");
            var tokenCode = Guid.NewGuid().ToString()[..8];
            await algorandExamples.CreateAssetTokenWithTaxonomyAsync(tokenCode, "BOB");
        }

        public static async Task AlgorandMultipleOperationsFlow(AlgorandExamples algorandExamples)
        {
            WriteToConsole("Create a new account for Bob");
            var bob = Guid.NewGuid().ToString();
            var bobsPrivateKey = await algorandExamples.CreateAccountAsync(bob);

            WriteToConsole("Create tokens that represent shares in the Mona Lisa and Nachtwacht paintings");
            var mlCode = Guid.NewGuid().ToString()[..8];
            var nwCode = Guid.NewGuid().ToString()[..8];

            var tokens = new Dictionary<string, string>
            {
                { mlCode, "MonaLisa" },
                { nwCode, "Nachtwacht" },
            };

            await algorandExamples.CreateAssetTokenMultipleAsync(tokens);

            WriteToConsole("Fund bobs account with Mona Lisa and Nachtwacht shares at the same time");
            var fundings = new Dictionary<string, decimal>
            {
                { mlCode, 1000 },
                { nwCode, 100 }
            };

            await algorandExamples.FundAccountMultipleAsync(bobsPrivateKey, fundings);
        }

        public static async Task AlgorandTokenLimitsFlow(AlgorandExamples algorandExamples)
        {
            WriteToConsole("Create a new token representing the Mona Lisa");
            var tokenCode = Guid.NewGuid().ToString()[..8];
            await algorandExamples.CreateAssetTokenAsync(tokenCode, "Mona Lisa");

            WriteToConsole("Create a new account for Bob");
            var bob = Guid.NewGuid().ToString();
            var bobsPrivateKey = await algorandExamples.CreateAccountAsync(bob);

            WriteToConsole("Create a new account for Alice");
            var alice = Guid.NewGuid().ToString();
            var alicesPrivateKey = await algorandExamples.CreateAccountAsync(alice);

            WriteToConsole("Now we Fund Bobs new account with 100 tokens");
            await algorandExamples.FundAccountAsync(bobsPrivateKey, tokenCode, 100);

            WriteToConsole("Then Bob sends 10 tokens to Alice.");
            await algorandExamples.PaymentAsync(bobsPrivateKey, alicesPrivateKey, tokenCode, 10);

            WriteToConsole("Getting funding limits for Bob");
            var tokenFundingLimitsResponse = await algorandExamples.GetTokenFundingLimits(bob, tokenCode);

            WriteToConsole("Total daily funding limits = " + tokenFundingLimitsResponse.Total.DailyLimit.ToString());
            WriteToConsole("Total monthly funding limits =" + tokenFundingLimitsResponse.Total.MonthlyLimit.ToString());
            WriteToConsole("Remaining daily funding limits = " + tokenFundingLimitsResponse.Remaining.DailyLimit.ToString());
            WriteToConsole("Remaining monthly funding limits = " + tokenFundingLimitsResponse.Remaining.DailyLimit.ToString());

            WriteToConsole("Bob waited for the tokens to increase in value and would like to get paid out now");
            await algorandExamples.PayoutAsync(bobsPrivateKey, tokenCode, 10);

            WriteToConsole("Getting payout limits for Bob");
            var tokenPayoutLimitsResponse = await algorandExamples.GetTokenPayoutLimits(bob, tokenCode);

            WriteToConsole("Total daily payout limits = " + tokenPayoutLimitsResponse.Total.DailyLimit.ToString());
            WriteToConsole("Total monthly payout limits =" + tokenPayoutLimitsResponse.Total.MonthlyLimit.ToString());
            WriteToConsole("Remaining daily payout limits = " + tokenPayoutLimitsResponse.Remaining.DailyLimit.ToString());
            WriteToConsole("Remaining monthly payout limits = " + tokenPayoutLimitsResponse.Remaining.DailyLimit.ToString());
        }

        public static async Task AlgorandUpdateTokenOperationStatusFlow(AlgorandExamples algorandExamples)
        {
            WriteToConsole("Create a new account for Bob");
            var bob = Guid.NewGuid().ToString();
            var bobsPrivateKey = await algorandExamples.CreateAccountAsync(bob);

            WriteToConsole("Create a new token representing the Mona Lisa");
            var tokenCode = Guid.NewGuid().ToString()[..8];
            await algorandExamples.CreateAssetTokenAsync(tokenCode, "Mona Lisa");

            WriteToConsole("Now we Fund Bobs new account with 100 tokens");
            await algorandExamples.FundAccountAsync(bobsPrivateKey, tokenCode, 100);

            WriteToConsole("Bob waited for the tokens to increase in value and would like to get paid out now");
            var payoutResponse = await algorandExamples.PayoutAsync(bobsPrivateKey, tokenCode, 10);

            WriteToConsole("Now we update Bob's payout operation status and payment reference");
            if (!string.IsNullOrEmpty(payoutResponse.PaymentCode))
            {
                await algorandExamples.UpdateOperationStatusAsync(payoutResponse.PaymentCode, "PayoutConfirming", "Algorand_SDK_Example_Bank_Reference_123");
            }
            else
            {
                WriteToConsole("Payment code is null or empty, cannot update operation status.", ConsoleColor.Red);
            }
        }

        private static void WriteToConsole(string message, ConsoleColor textColor = ConsoleColor.Green)
        {
            Console.ForegroundColor = textColor;
            Console.WriteLine(message);
        }
    }
}