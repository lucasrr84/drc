using collector.domain.entity;

namespace collector.domain.driver;

public interface IDriverFactory
{
    IDriver? Create(Ied ied);
}
