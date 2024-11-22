using collector.domain.entity;
using collector.domain.logger;

namespace collector.infra.logger;

public class ConsoleLog : ILog
{
    private bool _enabled = true;
    private bool _enabledDebugLog = true;

    public void Log(LogLevel logLevel, Ied? ied, string message)
    {
        if (!_enabled) return;

        string dateTime = $"{DateTime.Now:dd-MM-yyyy HH:mm:ss.fff} - ";
        string iedInfo = ied != null ? $"{ied.SubstationName.GetValue()}{ied.BayName.GetValue()} - " : "";
        string logMessage = $"{dateTime}{iedInfo}{message}";

        switch (logLevel)
        {
            case LogLevel.Information:
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[INFO] {logMessage}");
                break;

            case LogLevel.Error:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[ERROR] {logMessage}");
                break;

            case LogLevel.Warning:
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"[WARNING] {logMessage}");
                break;

            case LogLevel.Debug:
               Console.ForegroundColor = ConsoleColor.Cyan;
               if (_enabledDebugLog) Console.WriteLine($"[DEBUG] {logMessage}");
               break;
            
            default:
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"{logMessage}");
                break;
        }
    }

    public void SetEnable(bool status)
    {
        _enabled = status;
    }

    public void SetDebugLog(bool status)
    {
        _enabledDebugLog = status;
    }
}