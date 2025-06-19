using Alza.Convex.Logic.DIContainer;
using Alza.Convex.Logic.Interfaces;
using Microsoft.Extensions.DependencyInjection;

class Program
{
    private readonly IAppRunnerService _app;

    public Program(IAppRunnerService app)
    {
        _app = app;
    }

    public void Run() => _app.Run();

    static void Main()
    {
        var provider = DIContainer.GetServiceProvider();

        var program = ActivatorUtilities.CreateInstance<Program>(provider);
        program.Run();
    }
}
