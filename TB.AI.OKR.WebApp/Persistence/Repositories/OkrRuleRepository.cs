using Microsoft.EntityFrameworkCore;
using TB.AI.OKR.WebApp.Persistence.Contexts;
using TB.AI.OKR.WebApp.Persistence.Entities;

namespace TB.AI.OKR.WebApp.Persistence.Repositories
{
    public class OkrRuleRepository : IOkrRuleRepository
    {
        public OkrRuleRepository(ApplicationDbContext applicationDbContext)
        {
            ApplicationDbContext = applicationDbContext;
        }

        private ApplicationDbContext ApplicationDbContext { get; }


        public async Task<IEnumerable<OkrRule>> GetAllAsync()
        {
            var okrRules = await ApplicationDbContext.OkrRules
                .ToListAsync();
            return okrRules;
        }

        public async Task<bool> UpdateActiveStatus(int okrRuleId, bool activeStatus)
        {
            var okrRule = await ApplicationDbContext.OkrRules
                .FirstOrDefaultAsync(x => x.Id == okrRuleId);

            if (okrRule == null)
            {
                return false;
            }

            if (okrRule.IsActive == activeStatus)
            { 
                return true; 
            }
            
            okrRule.IsActive = activeStatus;

            var result = await ApplicationDbContext.SaveChangesAsync();

            return result > 0;
        }
    }
}
