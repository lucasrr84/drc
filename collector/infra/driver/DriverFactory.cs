using collector.domain.driver;
using collector.domain.gateway;

namespace collector.infra.driver;

public class DriverFactory : IDriverFactory
{
    private readonly IGatewayFactory _gatewayFactory;

    public DriverFactory(IGatewayFactory gatewayFactory)
    {
        _gatewayFactory = gatewayFactory;
    }
    
    public IDriver? Create(string manufacturer)
    {
        return manufacturer switch
        {
            "Abb" => new Abb(_gatewayFactory.Create("Mms")),

            "Alstom" => new Alstom(_gatewayFactory.Create("Mms")),

            "Areva" => new Areva(_gatewayFactory.Create("Mms")),

            "Efacec" => new Efacec(_gatewayFactory.Create("Mms")),

            "Schneider" => new Schneider(_gatewayFactory.Create("Mms")),

            "Sel" => new Sel(_gatewayFactory.Create("Telnet")),

            "Siemens" => new Siemens(_gatewayFactory.Create("Mms")),

            _ => null
        };
    }
}
