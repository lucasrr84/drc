using collector.domain.entity;
using collector.domain.gateway;
using collector.domain.logger;

namespace collector.infra.gateway;

public class SelGateway : IGateway
{
    private readonly ILog _logger;
    private readonly Ied _ied;

    public SelGateway(ILog logger, Ied ied)
    {
        _logger = logger;
        _ied = ied;
    }

    public async Task Connect()
    {
        _logger.Log(LogLevel.Debug, _ied, $"TELNET - Connecting...");
        //await Task.Delay(1000);
    }

    public async Task Disconnect()
    {
        _logger.Log(LogLevel.Debug, _ied, $"TELNET - Disconecting...");
        //await Task.Delay(1000);
    }

    public async Task<List<string>> GetDirectories(bool verbose)
    {
        _logger.Log(LogLevel.Debug, _ied, $"TELNET - Reading directories...");
        //await Task.Delay(1000);
        return new List<string>();
    }

    public async Task<List<string>> GetFiles(bool verbose)
    {
        _logger.Log(LogLevel.Debug, _ied, $"TELNET - Reading files...");
        //await Task.Delay(1000);
        return new List<string>();
    }

    public async Task Download(string fileName)
    {
        _logger.Log(LogLevel.Debug, _ied, $"TELNET - Downloading files...");
    }
}