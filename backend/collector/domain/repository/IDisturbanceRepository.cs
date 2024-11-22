using collector.domain.entity;

namespace collector.domain.repository;

public interface IDisturbanceRepository
{
    Task<IEnumerable<Disturbance>> GetAll();
    Task<Disturbance> GetById(string id);
    Task<IEnumerable<Disturbance>> GetBySubstationAndBay(string substation, string bay);
    Task<IEnumerable<Disturbance>> GetByDateTimeInterval(DateTime startDateTime, DateTime finishDateTime);
    Task<IEnumerable<Disturbance>> GetByDateTimeIntervalAndSubstationAndBay(DateTime startDateTime, DateTime finishDateTime, string substation, string bay);
    Task Save(Disturbance disturbance);
    Task Update(Disturbance disturbance);
    Task Delete(string id);
}
