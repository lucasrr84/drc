using collector.domain.dto;
using collector.domain.logger;
using collector.domain.repository;

namespace collector.application.usecase.ied;

public class GetEnabledIeds
{
    private readonly IIedRepository _iedRepository;
    private readonly ILog _logger;

    public GetEnabledIeds(IIedRepository iedRepository, ILog logger)
    {
        _iedRepository = iedRepository;
        _logger = logger;
    }

    public async Task<List<IedDto>> Execute()
    {
        var ieds = await _iedRepository.GetAllEnabled();

        if (ieds == null) throw new Exception($"GetEnabledIeds.Execute() - Enabled IEDs not found");
        
        _logger.Log(LogLevel.Information, null, $"{ieds.Count()} IEDs loaded");

        var result = new List<IedDto>();

        result.AddRange(ieds.Select(ied => new IedDto
        {
            Id = ied.Id.GetValue(),
            Manufacturer = ied.Manufacturer,
            Model = ied.Model,
            Enabled = ied.Enabled,
            SubstationName = ied.SubstationName.GetValue(),
            BayName = ied.BayName.GetValue(),
            LocalIpAddress = ied.LocalIpAddress,
            ExternalIpAddress = ied.ExternalIpAddress,
            TcpPort = ied.TcpPort
        }));

        return result;
    }
}