using collector.domain.entity;
using collector.domain.repository;

namespace collector.infra.repository;

public class IedRepositoryMemory : IIedRepository
{
    private readonly List<Ied> _ieds;

    public IedRepositoryMemory()
    {
        _ieds = new List<Ied>();
    }
    
    public async Task<IEnumerable<Ied>> GetAll()
    {
        return await Task.FromResult(_ieds);
    }

    public async Task<IEnumerable<Ied>> GetAllEnabled()
    {
        return await Task.FromResult(_ieds.Where(ied => ied.Enabled == true));
    }

    public async Task<Ied?> GetById(string id)
    {
        return await Task.FromResult(_ieds.FirstOrDefault(ied => ied.Id.GetValue() == id));
    }

    public Task Delete(string id)
    {
        throw new NotImplementedException();
    }

    public Task<Ied> GetBySubstationAndBay(string substation, string bay)
    {
        throw new NotImplementedException();
    }

    public async Task Save(Ied ied)
    {
        await Task.Run(() => _ieds.Add(ied));
    }

    public Task Update(Ied ied)
    {
        throw new NotImplementedException();
    }
}