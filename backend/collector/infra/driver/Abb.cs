using collector.domain.driver;
using collector.domain.gateway;

namespace collector.infra.driver;

public class Abb : IDriver
{
    private readonly IGateway _gateway;

    public Abb(IGateway gateway)
    {
        _gateway = gateway;
    }

    public async Task Connect()
    {
        await _gateway.Connect();
    }

    public async Task Disconnect()
    {
        await _gateway.Disconnect();
    }

    public async Task<List<string>> GetDirectories(bool verbose)
    {
        return await _gateway.GetDirectories(verbose);
    }

    public async Task<List<string>> GetFiles(bool verbose)
    {
        return await _gateway.GetFiles(verbose);
    }

    public async Task Download(string fileName)
    {
        await _gateway.Download(fileName);
    }

    public string GetFilePath(string fileName)
    {
        // Returns file path on IED in model: folder/file.cfg
        // Example: COMTRADE/file.cfg
        return $@"COMTRADE\{fileName.Split('\\')[^1]}";
    }
}