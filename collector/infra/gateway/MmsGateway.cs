using collector.domain.dto;
using collector.domain.gateway;
using collector.domain.logger;
using IEC61850.Client;

namespace collector.infra.gateway;

public class MmsGateway : IGateway
{
    private readonly ILog _logger;
    private readonly IedDto _ied;
    private IedConnection _iedConnection;

    public MmsGateway(ILog logger, IedDto ied)
    {
        _logger = logger;
        _ied = ied;
        //_iedConnection = new IedConnection();
    }

    public async Task Connect()
    {
        _logger.Log(LogLevel.Debug, _ied, $"MMS - Connecting...");
        //await Task.Delay(1000);
        //_iedConnection.ConnectTimeout = 20000;
        //_iedConnection.Connect(_ied.ExternalIpAddress, _ied.TcpPort);
    }

    public async Task Disconnect()
    {
        _logger.Log(LogLevel.Debug, _ied, $"MMS - Disconecting...");
        //await Task.Delay(1000);
        //await Task.Run(() => _iedConnection.AbortAsync());
        //_iedConnection.Dispose();
    }

    public async Task<List<string>> GetDirectories(bool verbose)
    {
        _logger.Log(LogLevel.Debug, _ied, $"MMS - Reading directories...");
        //await Task.Delay(1000);

        /*
        var existingDirectoryOnIed = await Task.Run(() => _iedConnection.GetServerDirectory(true));

        if (verbose)
        {
            foreach (string dir in existingDirectoryOnIed)
            {
                Console.WriteLine($"          -> {dir}");
            }
        }

        return existingDirectoryOnIed;
        */
        return new List<string>();
    }

    //public async Task<List<FileDirectoryEntry>> GetFiles(bool verbose)
    public async Task<List<string>> GetFiles(bool verbose)
    {
        _logger.Log(LogLevel.Debug, _ied, $"MMS - Reading files...");
        //await Task.Delay(1000);

        /*
        var files = await Task.Run(() => _iedConnection.GetFileDirectory(_iedPath));

        if (verbose)
        {
            foreach (var file in files)
            {
                Console.WriteLine($"          -> {file.GetFileName()}");
            }
        }

        return files;
        */
        //return new List<FileDirectoryEntry>();
        return new List<string>();
    }
}
