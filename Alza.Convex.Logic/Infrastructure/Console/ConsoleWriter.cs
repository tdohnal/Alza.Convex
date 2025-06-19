using Alza.Convex.Logic.Infrastructure.Interfaces;

namespace Alza.Convex.Logic.Infrastructure
{
    internal class ConsoleWriter<T> : IConsoleWriter<T>
    {
        private readonly string _source;

        public ConsoleWriter()
        {
            _source = typeof(T).Name;
        }

        public void Question(string message)
        {
            SetColor(ConsoleColor.Yellow);
            WriteWithPrefix("QUESTION", message, false);
        }

        public void Msg(string message)
        {
            SetColor(ConsoleColor.Cyan);
            WriteWithPrefix("MSG", message);
        }

        public void Warn(string message)
        {
            SetColor(ConsoleColor.Black, ConsoleColor.Yellow);
            WriteWithPrefix("WARN", message);
        }

        public void Error(string message)
        {
            SetColor(ConsoleColor.White, ConsoleColor.Red);
            WriteWithPrefix("ERROR", message);
        }

        public string Prompt(string message)
        {
            Question(message);
            return Console.ReadLine() ?? string.Empty;
        }

        public void WritePointsTable(IEnumerable<Point> points)
        {
            const int colWidth = 10;
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

        private void WriteWithPrefix(string level, string message, bool withLine = true)
        {
            var timestamp = DateTime.Now.ToString("HH:mm:ss");
            if (withLine)
            {
                Console.WriteLine($"[{timestamp}] [{level}] [{_source}] {message}");
            }
            else
            {
                Console.Write($"[{timestamp}] [{level}] [{_source}] {message}");
            }

            Console.ResetColor();
        }

        private void SetColor(ConsoleColor foreground, ConsoleColor? background = null)
        {
            Console.ForegroundColor = foreground;
            if (background.HasValue)
                Console.BackgroundColor = background.Value;
        }
    }
}
