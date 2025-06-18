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
    }

    private static void AddLogging(IServiceCollection services)
    {
        services.AddLogging(builder =>
        {
            builder.ClearProviders();
            builder.AddConsole();
            builder.SetMinimumLevel(LogLevel.Debug);
        });
    }
}
