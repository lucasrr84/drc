using collector.domain.entity;

namespace collector.domain.repository;

public interface IIedRepository
{
    Task<IEnumerable<Ied>> GetAll();
    Task<IEnumerable<Ied>> GetAllEnabled();
    Task<Ied?> GetById(string id);
    Task<Ied> GetBySubstationAndBay(string substation, string bay);
    Task Save(Ied ied);
    Task Update(Ied ied);
    Task Delete(string id);
}
