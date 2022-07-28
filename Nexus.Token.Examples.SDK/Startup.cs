using Microsoft.Extensions.DependencyInjection;
using Nexus.Token.SDK.Extensions;
using Serilog;

namespace Nexus.Token.Examples.SDK
{
    public class Startup
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
                .UseSymmetricEncryption("b14ca5898a4e4133bbce2ea2315a1916")
                .AddDefaultFundingPaymentMethod("FUNDING_EXAMPLE")
                .AddDefaultPayoutPaymentMethod("PAYOUT_EXAMPLE"));
        }
    }
}
