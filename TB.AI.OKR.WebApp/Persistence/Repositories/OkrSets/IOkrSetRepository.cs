using TB.AI.OKR.WebApp.Persistence.Entities;

namespace TB.AI.OKR.WebApp.Persistence.Repositories;

public interface IOkrSetRepository
{
    Task<IEnumerable<GetOkrSetsDto>> GetAllAsync();

    Task<OkrSet> AddAsync(AddOkrSetDto addOkrSet);

    Task<OkrSet?> GetByIdAsync(int id);
}
