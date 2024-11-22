using collector.domain.entity;
using collector.domain.gateway;
using collector.domain.logger;

namespace collector.infra.gateway;

public class GatewayFactory : IGatewayFactory
{
    private readonly ILog _logger;

    public GatewayFactory(ILog logger)
    {
        _logger = logger;
    }
    
    public IGateway Create(string protocol, Ied ied)
    {
        return protocol switch
        {
            "Mms" => new MmsGateway(_logger, ied),

            "Telnet" => new SelGateway(_logger, ied),

            _ => new MmsGateway(_logger, ied)
        };
    }
}