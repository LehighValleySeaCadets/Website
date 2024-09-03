using Api.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Api;

public class Program
{
    public static void Main()
    {
        var host = new HostBuilder()
            .ConfigureFunctionsWebApplication()
            .ConfigureServices(services => {
                services.AddApplicationInsightsTelemetryWorkerService();
                services.ConfigureFunctionsApplicationInsights();
                services.AddHttpClient("recaptcha", r =>
                {
                    r.BaseAddress = new Uri("https://www.google.com");
                });
                services.AddSingleton<IRecaptchaService, RecaptchaService>();
                services.AddLogging();
            })
    .Build();

        host.Run();
    }
}
