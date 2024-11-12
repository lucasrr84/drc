using collector.domain.driver;
using collector.domain.dto;
using collector.domain.gateway;

namespace collector.infra.driver;

public class DriverFactory : IDriverFactory
{
    private readonly IGatewayFactory _gatewayFactory;

    public DriverFactory(IGatewayFactory gatewayFactory)
    {
        _gatewayFactory = gatewayFactory;
    }
    
    public IDriver? Create(string manufacturer, IedDto iedDto)
    {
        return manufacturer switch
        {
            "Abb" => new Abb(_gatewayFactory.Create("Mms", iedDto)),

            "Alstom" => new Alstom(_gatewayFactory.Create("Mms", iedDto)),

            "Areva" => new Areva(_gatewayFactory.Create("Mms", iedDto)),

            "Efacec" => new Efacec(_gatewayFactory.Create("Mms", iedDto)),

            "Schneider" => new Schneider(_gatewayFactory.Create("Mms", iedDto)),

            "Sel" => new Sel(_gatewayFactory.Create("Telnet", iedDto)),

            "Siemens" => new Siemens(_gatewayFactory.Create("Mms", iedDto)),

            _ => null
        };
    }
}
