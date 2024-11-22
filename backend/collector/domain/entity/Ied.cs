using collector.domain.driver;
using collector.domain.vo;

namespace collector.domain.entity;

public class Ied
{
    public UUID Id { get; private set; }
    public string Manufacturer { get; private set; }
    public string Model { get; private set; }
    public bool Enabled {get; private set; }
    public Substation SubstationName { get; private set; }
    public Bay BayName { get; private set; }
    public string LocalIpAddress { get; private set; }
    public string ExternalIpAddress { get; private set; }
    public int TcpPort { get; private set; }
    public IDriver? Driver { get; private set; }

    public Ied(string iedId, string manufacturer, string model, string substation, string bay, string localIpAddress, string externalIpAddress, int tcpPort, bool status)
    {
        Id = new UUID(iedId);
        Manufacturer = manufacturer;
        Model = model;
        SubstationName = new Substation(substation);
        BayName = new Bay(bay);
        LocalIpAddress = localIpAddress;
        ExternalIpAddress = externalIpAddress;
        TcpPort = tcpPort;
        Enabled = status;
    }

    public static Ied Create(string manufacturer, string model, string substation, string bay, string localIpAddress, string externalIpAddress, int tcpPort, bool status)
    {
        var uuid = UUID.Create();
        return new Ied(uuid.GetValue(), manufacturer, model, substation, bay, localIpAddress, externalIpAddress, tcpPort, status);
    }

    public void SetManufacturer(string manufacturer)
    {
        Manufacturer = manufacturer;
    }

    public void SetModel(string model)
    {
        Model = model;
    }

    public void SetSubstation(string substation)
    {
        SubstationName = new Substation(substation);
    }

    public void SetBay(string bay)
    {
        BayName = new Bay(bay);
    }

    public void SetLocalIpAddress(string ipAddress)
    {
        LocalIpAddress = ipAddress;
    }

    public void SetExternalIpAddress(string ipAddress)
    {
        ExternalIpAddress = ipAddress;
    }

    public void SetTcpPort(int tcpPort)
    {
        TcpPort = tcpPort;
    }

    public void SetEnabled(bool status)
    {
        Enabled = status;
    }

    public void SetDriver(IDriver? driver)
    {
        Driver = driver;
    }
}
