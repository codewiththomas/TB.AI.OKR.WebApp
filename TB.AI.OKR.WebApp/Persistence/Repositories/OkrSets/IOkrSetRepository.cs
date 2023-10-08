using TB.AI.OKR.WebApp.Persistence.Entities;

namespace TB.AI.OKR.WebApp.Persistence.Repositories;

public interface IOkrSetRepository
{
    Task<OkrSet> AddAsync(AddOkrSetDto addOkrSet);

    Task<IEnumerable<GetOkrSetsDto>> GetAllAsync();

    Task<OkrSet?> GetByIdAsync(int id);

    Task<OkrSet> UpdateAsync(UpdateOkrSetDto updateOkrSetDto);
}
