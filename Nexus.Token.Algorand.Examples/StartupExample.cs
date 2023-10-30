using Microsoft.Extensions.DependencyInjection;
using Nexus.Sdk.Token.Extensions;
using Serilog;

namespace Nexus.Token.Algorand.Examples
{
    public class StartupExample
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();

            services.AddLogging(loggingBuilder =>
                loggingBuilder.AddSerilog(logger, dispose: true));

            services.AddTokenServer(o =>
               o.ConnectToTest("clientId", "clientSecret")
                .AddDefaultFundingPaymentMethod("FUNDING_EXAMPLE")
                .AddDefaultPayoutPaymentMethod("PAYOUT_EXAMPLE"));

            services.UseSymmetricEncryption("b14ca5898a4e4133bbce2ea2315a1916");
        }
    }
}
