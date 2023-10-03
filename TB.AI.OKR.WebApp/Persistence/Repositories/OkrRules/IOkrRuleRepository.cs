using TB.AI.OKR.WebApp.Persistence.Entities;

namespace TB.AI.OKR.WebApp.Persistence.Repositories;

public interface IOkrRuleRepository
{
    Task<IEnumerable<OkrRule>> GetAllAsync();
    Task<bool> UpdateActiveStatus(int okrRuleId, bool activeStatus);
}
