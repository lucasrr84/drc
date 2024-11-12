using collector.domain.dto;
using collector.domain.gateway;
using collector.domain.logger;

namespace collector.infra.gateway;

public class GatewayFactory : IGatewayFactory
{
    private readonly ILog _logger;
    private readonly IedDto _ied;

    public GatewayFactory(ILog logger, IedDto ied)
    {
        _logger = logger;
        _ied = ied;
    }
    
    public IGateway Create(string protocol)
    {
        return protocol switch
        {
            "Mms" => new MmsGateway(_logger, _ied),
            "Telnet" => new SelGateway(_logger, _ied),
            _ => new MmsGateway(_logger, _ied)
        };
    }
}
