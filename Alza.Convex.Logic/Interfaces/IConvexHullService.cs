using Alza.Convex.Logic.Models;

namespace Alza.Convex.Logic.Interfaces
{
    public interface IConvexHullService
    {
        IList<Point> FindConvexHull(IList<Point> points);

        IList<Point> GenerateRandomPoints(int count);
    }
}
