using TB.AI.OKR.Core.Domain;

namespace TB.AI.OKR.Core.Application;

public interface IReferenceSourceRepository
{
    Task<IEnumerable<GetReferenceSourcesDto>> GetAllAsync();

    Task<ReferenceSource> AddAsync(AddReferenceSourceDto addReferenceSourceDto);

    Task<bool> DeleteAsync(int id);

    Task<ReferenceSource?> GetByIdAsync(int id);

    Task<ReferenceSource> UpdateAsync(UpdateReferenceSourceDto updateRefernceSourceDto);
}
