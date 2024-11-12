
namespace collector.domain.dto;

public class IedDto
{
    public string Id { get; set; } = String.Empty;
    public string Manufacturer { get; set; } = String.Empty;
    public string Model { get; set; } = String.Empty;
    public bool Enabled {get; set; } = false;
    public string SubstationName { get; set; } = String.Empty;
    public string BayName { get; set; } = String.Empty;
    public string LocalIpAddress { get; set; } = String.Empty;
    public string ExternalIpAddress { get; set; } = String.Empty;
    public int TcpPort { get; set; } = 0;

}