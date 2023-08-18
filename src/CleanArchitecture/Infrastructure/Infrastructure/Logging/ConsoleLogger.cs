using Application.Logging;

namespace Infrastructure.Logging;
public class ConsoleLogger : ILogger
{
    Action<string> writeToConsole;

    public ConsoleLogger(Action<string> writeToConsole)
    {
        this.writeToConsole = writeToConsole;
    }

    public void Debug(string message)
    {
        throw new NotImplementedException();
    }

    public void Error(string message, Exception exception)
    {
        throw new NotImplementedException();
    }

    public void Fatal(string message, Exception exception)
    {
        throw new NotImplementedException();
    }

    public void Information(string message)
    {
        throw new NotImplementedException();
    }

    public void Warning(string message)
    {
        throw new NotImplementedException();
    }
}
