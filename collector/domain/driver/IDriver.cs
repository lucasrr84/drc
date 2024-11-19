
namespace collector.domain.driver;

public interface IDriver
{
    Task Connect();
    Task<List<string>> GetDirectories(bool verbose);
    Task<List<string>> GetFiles(bool verbose);
    Task Disconnect();
    string GetFilePath(string fileName);
}
