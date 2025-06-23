using Alza.Convex.Logic.Interfaces;
using Alza.Convex.Logic.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Alza.Convex.Tests.Tests;

public class ConvexHullTests : TestBase
{
    private readonly IConvexHullService _convexHullService;
    private readonly ILogger<ConvexHullTests> _logger;

    public ConvexHullTests()
    {
        _convexHullService = ServiceProvider.GetRequiredService<IConvexHullService>();

        _logger = ServiceProvider.GetRequiredService<ILogger<ConvexHullTests>>();
    }

    [Fact]
    public void FindsCorrectHull()
    {
        var points = new List<Point>
        {
            new Point(0, 0),
            new Point(1, 1),
            new Point(0, 1),
            new Point(1, 0),
            new Point(0, 0),
            new Point(0, 0)
        };

        _logger.LogInformation("Testing convex hull computation with {Count} points.", points.Count);

        var hull = _convexHullService.FindConvexHull(points);

        _logger.LogInformation("Computed convex hull with {HullCount} points.", hull.Count);

        Assert.Equal(4, hull.Count);
        Assert.Contains(new Point(0, 0), hull);
        Assert.Contains(new Point(0, 1), hull);
        Assert.Contains(new Point(1, 0), hull);
        Assert.Contains(new Point(1, 1), hull);
    }

    [Theory]
    [MemberData(nameof(TestCases))]
    public void ConvexHull_ReturnsExpectedResult(List<Point> input, List<Point> expectedHull)
    {
        var points = input.Distinct().ToList();
        var hull = _convexHullService.FindConvexHull(points);

        Assert.Equal(expectedHull.Count, hull.Count);
        foreach (var point in expectedHull)
            Assert.Contains(point, hull);
    }

    public static IEnumerable<object[]> TestCases =>
        new List<object[]>
        {
        new object[] {
            new List<Point> { new(0, 0), new(1, 1), new(2, 2) },
            new List<Point> { new(0, 0), new(2, 2) }
        },
        new object[] {
            new List<Point> { new(0, 0), new(0, 0), new(0, 0) },
            new List<Point> { new(0, 0) }
        },
        new object[] {
            new List<Point> { new(0, 0), new(0, 1), new(1, 0), new(1, 1) },
            new List<Point> { new(0, 0), new(0, 1), new(1, 1), new(1, 0) }
        }
        };

}
