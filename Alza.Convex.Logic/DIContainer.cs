using Alza.Convex.Logic.Infrastructure;
using Alza.Convex.Logic.Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;
namespace Alza.Convex.Logic.DIContainer;
public static class DIContainer
{
    public static IServiceProvider GetServiceProvider()
    {
        var services = new ServiceCollection();
        RegisterServices(services);

        var provider = services.BuildServiceProvider();

        return provider;
    }

    private static void RegisterServices(IServiceCollection services)
    {
        AddLogging(services);

        services.AddSingleton<IConvexHullService, ConvexHullService>();
        services.AddSingleton<IAppRunnerService, AppRunnerService>();
        services.AddSingleton(typeof(IConsoleWriter<>), typeof(ConsoleWriter<>));

    }

    private static void AddLogging(IServiceCollection services)
    {
        services.AddLogging(builder =>
        {
            builder.AddSimpleConsole(options =>
            {
                options.IncludeScopes = false;
                options.SingleLine = true;
                options.TimestampFormat = "HH:mm:ss ";
            });

            builder.ClearProviders();
            builder.AddConsole();
        });
    }
}
