using collector.domain.dto;
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
    
    public IGateway Create(string protocol, IedDto iedDto)
    {
        return protocol switch
        {
            "Mms" => new MmsGateway(_logger, iedDto),
            "Telnet" => new SelGateway(_logger, iedDto),
            _ => new MmsGateway(_logger, iedDto)
        };
    }
}
