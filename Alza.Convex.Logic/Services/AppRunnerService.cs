namespace Alza.Convex.Logic.Services
{
    public class AppRunnerService : IAppRunnerService
    {
        private readonly ILogger<AppRunnerService> _logger;
        private readonly IConvexHullService _convexHullService;

        public AppRunnerService(IConvexHullService convexHullService, ILogger<AppRunnerService> logger)
        {
            _convexHullService = convexHullService;
            _logger = logger;
        }

        public void Run()
        {
            _logger.LogDebug("Spouštím aplikaci pro nalezení konvexní obálky bodů.");

            Console.Write("Zadejte počet bodů:");
            if (!int.TryParse(Console.ReadLine(), out var n) || n < 3)
            {
                Console.WriteLine("Zadejte platné celé číslo větší nebo rovno 3.");
                return;
            }

            Console.WriteLine("Chcete body automaticky vygenerovat? (y/n):");
            var autoGen = Console.ReadLine()?.Trim().ToLower();

            IList<Point> points;

            if (autoGen == "y" || autoGen == "yes")
            {
                Console.Clear();
                _logger.LogInformation($"Automaticky generuji {n} bodů.");
                points = _convexHullService.GenerateRandomPoints(n);
            }
            else
            {
                points = ReadPointsFromConsole(n);
            }

            var hull = _convexHullService.FindConvexHull(points);

            Console.WriteLine("\nKonvexní obálka:");
            foreach (var p in hull)
            {
                Console.WriteLine($"({p.X}, {p.Y})");
            }

            _logger.LogInformation("Aplikace byla úspěšně dokončena.");
        }

        private List<Point> ReadPointsFromConsole(int count)
        {
            var points = new List<Point>();

            for (var i = 0; i < count; i++)
            {
                Console.WriteLine($"Zadej bod {i + 1} ve formátu X Y:");
                var input = Console.ReadLine()?.Split();

                if (input?.Length != 2 ||
                    !int.TryParse(input[0], out var x) ||
                    !int.TryParse(input[1], out var y))
                {
                    Console.WriteLine("Neplatný vstup. Zkus to znovu.");
                    i--;
                    continue;
                }

                points.Add(new Point(x, y));
            }

            return points;
        }
    }
}
