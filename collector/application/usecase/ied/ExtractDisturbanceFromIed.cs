using collector.domain.driver;
using collector.domain.dto;
using collector.domain.repository;

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

        //ied.SetDriver(new DriverFactory(_logger, iedDto).Create(ied.Manufacturer));
        ied.SetDriver(_driverFactory.Create(ied.Manufacturer));

        if (ied.Driver == null) throw new Exception($"ExtractDisturbanceFromIed.Execute() - {ied.SubstationName.GetValue()}{ied.BayName.GetValue()} - {ied.Manufacturer} Driver not found");
        
        // Conecta ao IED
        await ied.Driver.Connect();

        // Cria diretorio no repositorio
        //Directory.CreateDirectory($@"c:\caminho_repositorio\{ied.SubstationName.GetValue()}\{ied.BayName.GetValue()}\");

        // Lista diretorios do IED
        await ied.Driver.GetDirectories(false);

        // Lista arquivos do IED
        var filesInIed = await ied.Driver.GetFiles(false);

        //mais etapas...

        // Desconecta do IED
        await ied.Driver.Disconnect();
    }
}