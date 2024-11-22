using collector.domain.dto;
using collector.domain.repository;

namespace collector.application.usecase.ied;

public class GetIedById
{
    private readonly IIedRepository _iedRepository;

    public GetIedById(IIedRepository iedRepository)
    {
        _iedRepository = iedRepository;
    }

    public async Task<IedDto> Execute(string id)
    {
        var ied = await _iedRepository.GetById(id);

        if (ied == null) throw new Exception($"GetIedById.Execute() - {id} - IED not found");
        
        var result = new List<IedDto>();

        return new IedDto
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
        };
    }
}
