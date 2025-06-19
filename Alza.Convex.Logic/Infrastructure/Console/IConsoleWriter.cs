namespace Alza.Convex.Logic.Infrastructure;

public interface IConsoleWriter<T>
{
    void Msg(string message);
    void Warn(string message);
    void Error(string message);
    void Question(string message);
    string Prompt(string message);
    void WritePointsTable(IEnumerable<Point> points);
}
