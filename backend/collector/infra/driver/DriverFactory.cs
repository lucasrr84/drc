using collector.domain.driver;
using collector.domain.entity;
using collector.domain.gateway;

namespace collector.infra.driver;

public class DriverFactory : IDriverFactory
{
    private readonly IGatewayFactory _gatewayFactory;

    public DriverFactory(IGatewayFactory gatewayFactory)
    {
        _gatewayFactory = gatewayFactory;
    }
    
    public IDriver? Create(Ied ied)
    {
        return ied.Manufacturer switch
        {
            "Abb" => new Abb(_gatewayFactory.Create("Mms", ied)),

            "Alstom" => new Alstom(_gatewayFactory.Create("Mms", ied)),

            "Areva" => new Areva(_gatewayFactory.Create("Mms", ied)),

            "Efacec" => new Efacec(_gatewayFactory.Create("Mms", ied)),

            "Schneider" => new Schneider(_gatewayFactory.Create("Mms", ied)),

            "Sel" => new Sel(_gatewayFactory.Create("Telnet", ied)),

            "Siemens" => new Siemens(_gatewayFactory.Create("Mms", ied)),

            _ => null
        };
    }
}
