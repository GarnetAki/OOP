namespace Backups.Extra.Logging;

public class ConsoleLogger : ILogger
{
    public void WriteLine(string line)
    {
        System.Console.WriteLine(line);
    }
}