using TB.AI.OKR.Core.Domain;

namespace TB.AI.OKR.Core.Application;

public interface IOkrSetRepository
{
    Task<OkrSet> AddAsync(AddOkrSetDto addOkrSet);

    Task<IEnumerable<GetOkrSetsDto>> GetAllAsync();

    Task<OkrSet?> GetByIdAsync(int id);

    Task<OkrSet> UpdateAsync(UpdateOkrSetDto updateOkrSetDto);
}
