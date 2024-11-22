using collector.domain.entity;

namespace collector.domain.gateway;

public interface IGatewayFactory
{
    IGateway Create(string protocol, Ied ied);
}