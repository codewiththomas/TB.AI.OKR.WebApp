using TB.AI.OKR.WebApp.Dtos;
using TB.AI.OKR.WebApp.Persistence.Entities;

namespace TB.AI.OKR.WebApp.Persistence.Repositories;

public interface IOkrRepository
{
    Task<IEnumerable<Okr>> GetAllAsync();

    Task<Okr> AddAsync(Okr okr);

    Task<Okr?> GetByIdAsync(int id);

    Task<Okr?> UpdateAsync(UpdateOkrDto updateOkrDto);
}
