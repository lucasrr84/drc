using collector.domain.driver;
using collector.domain.gateway;

namespace collector.infra.driver;

public class Sel : IDriver
{
    private readonly IGateway _gateway;

    public Sel(IGateway gateway)
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
        //Retorna caminho do arquivo no IED no modelo pasta/arquivo.cfg
        //Exemplo: COMTRADE/arquivo.CEV
        return $@"COMTRADE\{fileName}";
    }
}
