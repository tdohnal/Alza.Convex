using Alza.Convex.Logic.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Drawing;

namespace Alza.Convex.Tests.Tests
{
    public class ConvexHullTests : TestBase
    {
        [Fact]
        public void FindsCorrectHull()
        {
            List<Point> points = new List<Point>
{
                new Point(0, 0),
                new Point(1, 1),
                new Point(0, 1),
                new Point(1, 0),
                new Point(0, 0),
                new Point(0, 0)
};

            var convexHullService = ServiceProvider.GetRequiredService<IConvexHullService>();
            var hull = convexHullService.FindConvexHull(points);

            Assert.Equal(4, hull.Count);
            Assert.Contains(new Point(0, 0), hull);
            Assert.Contains(new Point(0, 1), hull);
            Assert.Contains(new Point(1, 0), hull);
            Assert.Contains(new Point(1, 1), hull);
        }
    }
}
