
namespace collector.domain.gateway;

public interface IGatewayFactory
{
    IGateway Create(string protocol);
}
