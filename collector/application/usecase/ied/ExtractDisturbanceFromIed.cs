using collector.domain.driver;
using collector.domain.dto;
using collector.domain.repository;
using collector.domain.service;

namespace collector.application.usecase.ied;

public class ExtractDisturbanceFromIed
{
    private IIedRepository _iedRepository;
    private readonly IDriverFactory _driverFactory;

    public ExtractDisturbanceFromIed(IIedRepository iedRepository, IDriverFactory driverFactory)
    {
        _iedRepository = iedRepository;
        _driverFactory = driverFactory;
    }

    public async Task Execute(string iedId)
    {
        var ied = await _iedRepository.GetById(iedId);

        if (ied == null) throw new Exception($"ExtractDisturbanceFromIed.Execute() - {iedId} - IED not found");

        var iedDto = new IedDto
        {
            Id = ied.Id.GetValue(),
            Manufacturer = ied.Manufacturer,
            Model = ied.Model,
            Enabled = ied.Enabled,
            SubstationName = ied.SubstationName.GetValue(),
            BayName = ied.BayName.GetValue(),
            LocalIpAddress = ied.LocalIpAddress,
            ExternalIpAddress = ied.ExternalIpAddress,
            TcpPort = ied.TcpPort,
        };

        ied.SetDriver(_driverFactory.Create(ied.Manufacturer, iedDto));

        if (ied.Driver == null) throw new Exception($"ExtractDisturbanceFromIed.Execute() - {ied.SubstationName.GetValue()}{ied.BayName.GetValue()} - {ied.Manufacturer} Driver not found");
        
        // Conecta ao IED
        await ied.Driver.Connect();

        // Cria diretorio no repositorio
        //Directory.CreateDirectory($@"c:\caminho_repositorio\{ied.SubstationName.GetValue()}\{ied.BayName.GetValue()}\");

        // Lista diretorios do IED
        await ied.Driver.GetDirectories(false);

        // Lista arquivos do IED
        var filesInIed = await ied.Driver.GetFiles(false);

        // Seleciona os novos arquivos
        var newFiles = filesInIed.Where(file => NewFileDetectorService.IsNewFile(file)).ToList();

        // Conta quantos arquivos novos existem

        // Realiza o download
        newFiles.Select(file => DownloadService.Execute(file));
        
        // Le arquivo cfg para pegar data/hora - errado. modificar para ler do arquivo ja salvo no hd
        newFiles.Select(file => GetDateTimeFromFile.Execute(file));
        
        // Cria oscilografia

        
        // Grava no BD
        
        // Desconecta do IED
        await ied.Driver.Disconnect();
    }
}