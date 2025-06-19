namespace Alza.Convex.Logic.Services
{
    public class ConvexHullService : IConvexHullService
    {
        private readonly ILogger<ConvexHullService> _logger;

        public ConvexHullService(
            ILogger<ConvexHullService> logger)
        {
            _logger = logger;
        }

        public IList<Point> FindConvexHull(IList<Point> points)
        {
            if (points.Count <= 1)
                return points;

            var sorted = points.OrderBy(p => p.X).ThenBy(p => p.Y).ToList();

            List<Point> lower = new();
            foreach (var p in sorted)
            {
                while (lower.Count >= 2 && Cross(lower[^2], lower[^1], p) <= 0)
                    lower.RemoveAt(lower.Count - 1);
                lower.Add(p);
            }

            List<Point> upper = new();
            for (int i = sorted.Count - 1; i >= 0; i--)
            {
                var p = sorted[i];
                while (upper.Count >= 2 && Cross(upper[^2], upper[^1], p) <= 0)
                    upper.RemoveAt(upper.Count - 1);
                upper.Add(p);
            }

            lower.RemoveAt(lower.Count - 1);
            upper.RemoveAt(upper.Count - 1);

            return lower.Concat(upper).ToList();
        }

        public IList<Point> GenerateRandomPoints(int count)
        {
            var rnd = new Random();
            var points = new List<Point>(count);
            for (var i = 0; i < count; i++)
            {
                var x = rnd.Next(0, 101);
                var y = rnd.Next(0, 101);

                var item = new Point(x, y);
                points.Add(item);

                _logger.LogInformation($"Generate point {i + 1}: ({x}, {y})...");
            }
            return points;
        }

        private int Cross(Point o, Point a, Point b)
        {
            return (a.X - o.X) * (b.Y - o.Y) - (a.Y - o.Y) * (b.X - o.X);
        }
    }
}