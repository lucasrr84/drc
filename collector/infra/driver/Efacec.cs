using collector.domain.driver;
using collector.domain.gateway;

namespace collector.infra.driver;

public class Efacec : IDriver
{
    public string IedDirectory { get; private set; } = $@"COMTRADE\";
    private readonly IGateway _gateway;

    public Efacec(IGateway gateway)
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

    public string GetFilePath(string fileName)
    {
        //Retorna caminho do arquivo no IED no modelo pasta/arquivo.cfg
        //Exemplo: /COMTRADE//arquivo.cfg
        return $"{IedDirectory}{fileName.Split('/')[^1]}";
    }
}
