using collector.domain.config;
using collector.domain.entity;
using collector.domain.gateway;
using collector.domain.logger;
using IEC61850.Client;

namespace collector.infra.gateway;

public class MmsGateway : IGateway
{
    private readonly ILog _logger;
    private readonly Ied _ied;
    private IedConnection _iedConnection;

    public MmsGateway(ILog logger, Ied ied)
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

        var existingDirectoryOnIed = await Task.Run(() => _iedConnection.GetServerDirectory(true));

        if (verbose)
        {
            foreach (string dir in existingDirectoryOnIed)
                _logger.Log(LogLevel.Information, _ied, $"          -> {dir}");
        }

        return existingDirectoryOnIed;
    }

    public async Task<List<string>> GetFiles(bool verbose)
    {
        _logger.Log(LogLevel.Debug, _ied, $"MMS - Reading files...");

        //await Task.Delay(1000);
        /*
        var files = await Task.Run(() => _iedConnection.GetFileDirectory(_driver.IedDirectory));

        if (verbose)
        {
            foreach (var file in files)
                _logger.Log(LogLevel.Information, _ied, $"          -> {file.GetFileName()}");
        }

        return files.Select(file => file.GetFileName()).ToList();
        */
        return new List<string>();
    }

    public async Task Download(string fileName)
    {
        try
        {
            _logger.Log(LogLevel.Information, _ied, $"MMS - Downloading {fileName}...");
            
            string iedFileName = _ied.Driver!.GetFilePath(fileName);
            string caminhoArquivoServidor = $@"{Config.ServerPathDirectory}{_ied.SubstationName.GetValue()}{_ied.BayName.GetValue()}\{fileName}";

            FileStream fs = new FileStream(caminhoArquivoServidor, FileMode.Create);
            BinaryWriter w = new BinaryWriter(fs);
            bool fileDownloadFinished = false;
            /*
            _iedConnection.GetFileAsync(iedFileName, delegate (UInt32 invokeId, object parameter, IedClientError err, UInt32 originalInvokeId, byte[] buffer, bool moreFollows)
            {
                if (err == IedClientError.IED_ERROR_OK)
                {
                    w.Write(buffer);
                    if (moreFollows == false) fileDownloadFinished = true;
                }
                else
                {
                    _logger.Log(LogLevel.Error, _ied, $"MMS - File download error: {err}");
                    fileDownloadFinished = true;
                }

                return true;
            }, w);

            while (fileDownloadFinished == false)
            {
                Thread.Sleep(500);
            }

            fs.Close();
            _logger.Log(LogLevel.Information, _ied, $"MMS - Download file {fileName} finished");
            */
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Error, _ied, ex.Message);
        }
    }
}