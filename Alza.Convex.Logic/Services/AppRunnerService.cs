namespace Alza.Convex.Logic.Services;

public class AppRunnerService : IAppRunnerService
{
    private readonly ILogger<AppRunnerService> _logger;
    private readonly IConvexHullService _convexHullService;

    public AppRunnerService(
        IConvexHullService convexHullService,
        ILogger<AppRunnerService> logger)
    {
        _convexHullService = convexHullService;
        _logger = logger;
    }

    public void Run()
    {
        try
        {
            Console.Write("Enter number of points: ");
            var inputN = Console.ReadLine();
            if (!int.TryParse(inputN, out var n) || n < 3)
            {
                _logger.LogWarning("Please enter a valid integer (3 or more).");
                return;
            }


            Console.Write("Do you want to generate points automatically? (y/n): ");
            var autoGenInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(autoGenInput))
            {
                _logger.LogWarning("Input cannot be null or empty.");
                return;
            }
            var autoGen = autoGenInput.Trim().ToLower();


            IList<Point> points;

            if (autoGen == "y" || autoGen == "yes")
            {
                _logger.LogDebug($"Generating {n} random points...");
                _logger.LogInformation("Random generation of {Count} points.", n);
                points = _convexHullService.GenerateRandomPoints(n);
            }
            else
            {
                points = ReadPointsFromConsole(n);
            }

            _logger.LogDebug("Starting convex hull calculation.");

            var hull = _convexHullService.FindConvexHull(points);

            WritePointsTable(hull);

            _logger.LogInformation("Application finished successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception thrown in AppRunnerService.");
        }
    }

    private List<Point> ReadPointsFromConsole(int count)
    {
        var points = new List<Point>();

        for (int i = 0; i < count; i++)
        {
            Console.Write($"Enter point {i + 1} (X Y): ");
            var input = Console.ReadLine().Split();

            if (input.Length != 2 ||
                !int.TryParse(input[0], out var x) ||
                !int.TryParse(input[1], out var y))
            {
                _logger.LogWarning("Invalid input for point {Index}.", i + 1);
                i--;
                continue;
            }

            points.Add(new Point(x, y));
            _logger.LogDebug("User entered point: ({X}, {Y})", x, y);
        }

        return points;
    }

    public void WritePointsTable(IEnumerable<Point> points)
    {
        Console.WriteLine("\nConvex Hull:");
        const int colWidth = 12;
        string separator = "+" + new string('-', colWidth + 2) + "+" + new string('-', colWidth + 2) + "+";

        SetColor(ConsoleColor.Cyan);
        Console.WriteLine(separator);
        Console.WriteLine($"| {"X".PadRight(colWidth)} | {"Y".PadRight(colWidth)} |");
        Console.WriteLine(separator);
        Console.ResetColor();

        foreach (var p in points)
        {
            Console.WriteLine($"| {p.X.ToString().PadRight(colWidth)} | {p.Y.ToString().PadRight(colWidth)} |");
        }

        SetColor(ConsoleColor.Cyan);
        Console.WriteLine(separator);
        Console.ResetColor();
    }

    private void SetColor(ConsoleColor foreground, ConsoleColor? background = null)
    {
        Console.ForegroundColor = foreground;
        if (background.HasValue)
            Console.BackgroundColor = background.Value;
    }
}
