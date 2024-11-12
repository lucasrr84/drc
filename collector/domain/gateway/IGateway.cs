
namespace collector.domain.gateway;

public interface IGateway
{
    Task Connect();
    Task Disconnect();
    Task<List<string>> GetDirectories(bool verbose);
    Task<List<string>> GetFiles(bool verbose);
}
