using Alza.Convex.Logic.DIContainer;
using Alza.Convex.Logic.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

class Program
{
    private readonly ILogger<Program> _logger;
    private readonly IAppRunnerService _app;

    public Program(ILogger<Program> logger, IAppRunnerService app)
    {
        _logger = logger;
        _app = app;
    }

    public void Run()
    {
        _logger.LogInformation("Spouštím aplikaci...");
        _app.Run();
        _logger.LogInformation("Aplikace dokončena.");
    }

    static void Main()
    {
        var provider = DIContainer.GetServiceProvider();

        var program = ActivatorUtilities.CreateInstance<Program>(provider);
        program.Run();
    }
}
