using TB.AI.OKR.WebApp.Persistence.Entities;

namespace TB.AI.OKR.WebApp.Persistence.Repositories;

public interface IReferenceSourceRepository
{
    Task<IEnumerable<GetReferenceSourcesDto>> GetAllAsync();

    Task<ReferenceSource> AddAsync(AddReferenceSourceDto addReferenceSourceDto);

    Task<bool> DeleteAsync(int id);

    Task<ReferenceSource?> GetByIdAsync(int id);

    Task<ReferenceSource> UpdateAsync(UpdateReferenceSourceDto updateRefernceSourceDto);
}
