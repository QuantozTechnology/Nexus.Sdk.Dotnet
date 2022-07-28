using Microsoft.Extensions.DependencyInjection;
using Nexus.SDK.Shared.Authentication;
using Nexus.SDK.Shared.Responses;
using Nexus.Token.Examples.SDK;
using Nexus.Token.SDK.Extensions;
using Serilog;

var logger = new LoggerConfiguration()
              .MinimumLevel.Debug()
              .WriteTo.Console()
              .CreateLogger();

var services = new ServiceCollection();

services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(logger, dispose: true));

services.AddTokenServer(o =>
    o.ConnectToTest("clientId", "clientSecret")
     .UseSymetricEncryption("b14ca5898a4e4133bbce2ea2315a1916")
     .AddDefaultFundingPaymentMethod("FUNDING_EXAMPLE")
     .AddDefaultPayoutPaymentMethod("PAYOUT_EXAMPLE"));

services.AddSingleton<AlgorandExample>();

var provider = services.BuildServiceProvider();

async Task AlgorandFlow()
{
    var algorandExample = provider.GetRequiredService<AlgorandExample>();

    // Create a new customers with an account
    var senderPrivateKey = await algorandExample.CreateAccountFlowAsync("SENDER_CUSTOMER");
    var receiverPrivateKey = await algorandExample.CreateAccountFlowAsync("RECEIVER_CUSTOMER");

    // Create a new token that is pegged to the mona lisa painting
    await algorandExample.CreateAssetTokenFlowAsync("MONALISA", "Mona Lisa");

    // Fund the one account with the token
    await algorandExample.FundAccountFlowAsync(senderPrivateKey, "MONALISA", 100);

    // Pay the token to another account
    await algorandExample.PaymentFlowAsync(senderPrivateKey, receiverPrivateKey, "MONALISA", 10);

    // Let the other account payout their token
    await algorandExample.PayoutFlowAsync(receiverPrivateKey, "MONALISA", 10);
}

try
{
    await AlgorandFlow();
}
catch (NexusApiException ex)
{
    Console.WriteLine($"{ex.StatusCode}");
    Console.WriteLine($"{ex.Message}");
    Console.WriteLine($"{ex.ErrorCodes}");
}
catch (AuthProviderException ex)
{
    Console.WriteLine($"{ex.Message}");
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

