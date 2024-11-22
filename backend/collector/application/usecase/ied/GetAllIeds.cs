using collector.domain.dto;
using collector.domain.repository;

namespace collector.application.usecase.ied;

public class GetAllIeds
{
    private readonly IIedRepository _iedRepository;

    public GetAllIeds(IIedRepository iedRepository)
    {
        _iedRepository = iedRepository;
    }

    public async Task<List<IedDto>> Execute()
    {
        var ieds = await _iedRepository.GetAll();

        if (ieds == null) throw new Exception("GetAllIeds.Execute() - IEDs not found");
        
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
