
namespace collector.domain.driver;

public interface IDriverFactory
{
    IDriver? Create(string manufacturer);
}
