using collector.domain.config;
using collector.domain.driver;
using collector.domain.entity;
using collector.domain.repository;
using collector.domain.service;

namespace collector.application.usecase.ied;

public class ExtractDisturbanceFromIed
{
    private readonly IIedRepository _iedRepository;
    private readonly IDisturbanceRepository _disturbanceRepository;
    private readonly IDriverFactory _driverFactory;

    public ExtractDisturbanceFromIed(IIedRepository iedRepository, IDisturbanceRepository disturbanceRepository, IDriverFactory driverFactory)
    {
        _iedRepository = iedRepository;
        _disturbanceRepository = disturbanceRepository;
        _driverFactory = driverFactory;
    }

    public async Task Execute(string iedId)
    {
        var ied = await _iedRepository.GetById(iedId);
        if (ied == null) throw new Exception($"ExtractDisturbanceFromIed.Execute() - {iedId} - IED not found");

        ied.SetDriver(_driverFactory.Create(ied));
        if (ied.Driver == null) throw new Exception($"ExtractDisturbanceFromIed.Execute() - {ied.SubstationName.GetValue()}{ied.BayName.GetValue()} - {ied.Manufacturer} Driver not found");
        
        Directory.CreateDirectory($@"{Config.ServerPathDirectory}{ied.SubstationName.GetValue()}{ied.BayName.GetValue()}\");
        
        await ied.Driver.Connect();
        //await ied.Driver.GetDirectories(false);
        var filesInIed = await ied.Driver.GetFiles(false);
        
        var newFileDetector = NewFileDetectorFactory.create("existingFile");
        var newFiles = filesInIed.Where(file => newFileDetector.IsNewFile(file, ied.SubstationName.GetValue(), ied.BayName.GetValue())).ToList();
        // Conta quantos arquivos novos existem
        newFiles.Select(file => ied.Driver.Download(file));
        await ied.Driver.Disconnect();

        newFiles.Select(async file =>
        {
            var triggerDateTime = await GetDateTimeFromFile.Execute(file);
            var disturbance = Disturbance.Create(triggerDateTime, ied.Id.GetValue());
            await _disturbanceRepository.Save(disturbance);
        });
    }
}