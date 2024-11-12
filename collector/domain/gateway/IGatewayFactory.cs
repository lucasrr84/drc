using collector.domain.dto;

namespace collector.domain.gateway;

public interface IGatewayFactory
{
    IGateway Create(string protocol, IedDto iedDto);
}
