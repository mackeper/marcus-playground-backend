namespace SharedKernel.Logging;
public interface ILogger
{
    void Information(string message);
    void Debug(string message);
    void Warning(string message);
    void Error(string message, Exception exception);
    void Fatal(string message, Exception exception);
}
