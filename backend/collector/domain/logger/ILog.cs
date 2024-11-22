using collector.domain.entity;

namespace collector.domain.logger;

public interface ILog
{
    void Log(LogLevel logLevel, Ied? ied, string message);
    void SetEnable(bool status);
    void SetDebugLog(bool status);
}
