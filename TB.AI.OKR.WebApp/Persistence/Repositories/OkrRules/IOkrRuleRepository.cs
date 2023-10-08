using TB.AI.OKR.WebApp.Persistence.Entities;

namespace TB.AI.OKR.WebApp.Persistence.Repositories;

public interface IOkrRuleRepository
{
    Task<OkrRule> AddAsync(AddOkrRuleDto addOkrRuleDto);

    Task<OkrRule?> GetAsync(int id);
    Task<IEnumerable<OkrRule>> GetAllAsync();
    Task<OkrRule> UpdateAsync(UpdateOkrRuleDto updateOkrRuleDto);
    Task<bool> UpdateActiveStatus(int okrRuleId, bool activeStatus);
}
