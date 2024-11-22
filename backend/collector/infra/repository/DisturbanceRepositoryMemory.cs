using collector.domain.entity;
using collector.domain.repository;

namespace collector.infra.repository;

public class DisturbanceRepositoryMemory : IDisturbanceRepository
{
    public Task Delete(string id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Disturbance>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Disturbance>> GetByDateTimeInterval(DateTime startDateTime, DateTime finishDateTime)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Disturbance>> GetByDateTimeIntervalAndSubstationAndBay(DateTime startDateTime, DateTime finishDateTime, string substation, string bay)
    {
        throw new NotImplementedException();
    }

    public Task<Disturbance> GetById(string id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Disturbance>> GetBySubstationAndBay(string substation, string bay)
    {
        throw new NotImplementedException();
    }

    public Task Save(Disturbance disturbance)
    {
        throw new NotImplementedException();
    }

    public Task Update(Disturbance disturbance)
    {
        throw new NotImplementedException();
    }
}
