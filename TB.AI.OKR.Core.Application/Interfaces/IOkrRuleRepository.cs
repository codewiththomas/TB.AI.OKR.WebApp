using TB.AI.OKR.Core.Domain;

namespace TB.AI.OKR.Core.Application;

public interface IOkrRuleRepository
{
    Task<OkrRule> AddAsync(AddOkrRuleDto addOkrRuleDto);

    Task<OkrRule?> GetAsync(int id);
    Task<IEnumerable<OkrRule>> GetAllAsync(GetOkrRulesFilter? filter = null);
    Task<OkrRule> UpdateAsync(UpdateOkrRuleDto updateOkrRuleDto);
    Task<bool> UpdateActiveStatus(int okrRuleId, bool activeStatus);
}
