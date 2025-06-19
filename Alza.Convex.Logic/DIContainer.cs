using Alza.Convex.Logic.Infrastructure;
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
        #region Logging

        AddLogging(services);

        #endregion

        #region Services

        services.AddSingleton<IConvexHullService, ConvexHullService>();
        services.AddSingleton<IAppRunnerService, AppRunnerService>();
        services.AddSingleton(typeof(IConsoleWriter<>), typeof(ConsoleWriter<>));

        #endregion
    }

    private static void AddLogging(IServiceCollection services)
    {
        services.AddLogging(builder =>
        {
            builder.ClearProviders();

#if DEBUG
            builder.SetMinimumLevel(LogLevel.Debug);
#endif
            builder.AddSimpleConsole(options =>
            {
                options.IncludeScopes = false;
                options.SingleLine = true;
                options.TimestampFormat = "HH:mm:ss ";
            });
        });
    }

}
