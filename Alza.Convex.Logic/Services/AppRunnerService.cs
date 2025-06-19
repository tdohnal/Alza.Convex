using Alza.Convex.Logic.Infrastructure.Interfaces;

namespace Alza.Convex.Logic.Services;

public class AppRunnerService : IAppRunnerService
{
    private readonly IConsoleWriter<AppRunnerService> _console;
    private readonly ILogger<AppRunnerService> _logger;
    private readonly IConvexHullService _convexHullService;

    public AppRunnerService(
        IConvexHullService convexHullService,
        IConsoleWriter<AppRunnerService> console,
        ILogger<AppRunnerService> logger)
    {
        _convexHullService = convexHullService;
        _console = console;
        _logger = logger;
    }

    public void Run()
    {
        try
        {
            _logger.LogDebug("Application Run - Convex Hull Calculator");

            var inputN = _console.Prompt("Enter number of points: ");
            if (!int.TryParse(inputN, out var n) || n < 3)
            {
                _console.Warn("Please enter a valid integer (3 or more).");
                return;
            }

            var autoGen = _console.Prompt("Do you want to generate points automatically? (y/n): ").Trim().ToLower();

            IList<Point> points;

            if (autoGen == "y" || autoGen == "yes")
            {
                _console.Msg($"Generating {n} random points...");
                _logger.LogInformation($"Random generation of {n} points.");
                points = _convexHullService.GenerateRandomPoints(n);
            }
            else
            {
                points = ReadPointsFromConsole(n);
            }

            _logger.LogDebug("Starting convex hull calculation.");

            var hull = _convexHullService.FindConvexHull(points);

            _console.Msg("\nConvex Hull:");
            _console.WritePointsTable(hull);

            _logger.LogInformation("Application finished successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception thrown in AppRunnerService.");
            _console.Error("An unexpected error occurred. Check logs.");
        }
    }

    private List<Point> ReadPointsFromConsole(int count)
    {
        var points = new List<Point>();

        for (int i = 0; i < count; i++)
        {
            var input = _console.Prompt($"Enter point {i + 1} (X Y): ").Split();

            if (input.Length != 2 ||
                !int.TryParse(input[0], out var x) ||
                !int.TryParse(input[1], out var y))
            {
                _console.Warn("Invalid input. Try again.");
                _logger.LogWarning($"Invalid input for point {i + 1}.");
                i--;
                continue;
            }

            points.Add(new Point(x, y));
            _logger.LogDebug($"User entered point: ({x}, {y})");
        }

        return points;
    }
}
