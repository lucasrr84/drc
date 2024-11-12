using collector.domain.dto;

namespace collector.domain.logger;

public interface ILog
{
    void Log(LogLevel logLevel, IedDto? ied, string message);
    void SetEnable(bool status);
    void SetDebugLog(bool status);
}
