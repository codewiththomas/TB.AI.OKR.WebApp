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


        /// <summary>
        /// Get all OKR rules
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<OkrRule>> GetAllAsync(GetOkrRulesFilter? filter)
        {
            var okrRulesQuery = ApplicationDbContext.OkrRules
                .Include(x => x.References)
                .AsQueryable();

            if (filter is not null)
            {
                if (filter.IsActive is not null)
                {
                    okrRulesQuery = okrRulesQuery
                        .Where(x => x.IsActive == filter.IsActive)
                        .AsQueryable();
                }                
            }

            return await okrRulesQuery.ToListAsync();
        }


        public async Task<OkrRule?> GetAsync(int id)
        {
            var okrRule = await ApplicationDbContext.OkrRules
                .Include(x => x.References)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            return okrRule;
        }


        public async Task<OkrRule> AddAsync(AddOkrRuleDto addOkrRuleDto)
        {
            using var transaction = await ApplicationDbContext.Database.BeginTransactionAsync();

            try
            {
                /* add OKR rule */
                var newOkrRuleEntity = new OkrRule
                {
                    Title = addOkrRuleDto.OkrRuleTitle,
                    Description = addOkrRuleDto.OkrRuleText,
                    Scope = addOkrRuleDto.Scope,
                    Severity = addOkrRuleDto.Severity,
                    IsActive = true
                };
                await ApplicationDbContext.AddAsync(newOkrRuleEntity);
                var result = await ApplicationDbContext.SaveChangesAsync();

                /* add references */
                foreach (var referenceId in addOkrRuleDto.ReferenceSourceIds)
                {
                    var existingReference = await ApplicationDbContext.ReferenceSources
                        .Include(x => x.OkrRules)
                        .FirstOrDefaultAsync(x => x.Id == referenceId);

                    if (existingReference is null)
                    {
                        continue;
                    }

                    if (existingReference.OkrRules is null)
                    {
                        existingReference.OkrRules = new List<OkrRule>();
                    }

                    existingReference!.OkrRules.Add(newOkrRuleEntity);
                    await ApplicationDbContext.SaveChangesAsync();
                }

                await transaction.CommitAsync();

                return newOkrRuleEntity;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }


        public async Task<OkrRule> UpdateAsync(UpdateOkrRuleDto updateOkrRuleDto)
        {
            var existingOkrRule = await ApplicationDbContext.OkrRules
                .Where(x => x.Id == updateOkrRuleDto.Id)
                .Include(r => r.References)
                .FirstOrDefaultAsync();

            if (existingOkrRule is null)
            {
                throw new Exception($"Can't update. OKR rule with Id = {updateOkrRuleDto.Id} not found.");
            }

            using var transaction = await ApplicationDbContext.Database.BeginTransactionAsync();

            try
            {
                existingOkrRule.Title = updateOkrRuleDto.OkrRuleTitle;
                existingOkrRule.Description = updateOkrRuleDto.OkrRuleText;
                existingOkrRule.Scope = updateOkrRuleDto.Scope;
                existingOkrRule.Severity = updateOkrRuleDto.Severity;
                await ApplicationDbContext.SaveChangesAsync();

                var existingReferenceIds = existingOkrRule.References.Select(x => x.Id).ToList();
                var newReferenceIds = updateOkrRuleDto.ReferenceSourceIds;

                var deletedReferenceIds = existingReferenceIds.Except(newReferenceIds);
                foreach (var deletedReferenceId in deletedReferenceIds)
                {
                    var itemToRemove = existingOkrRule.References.First(x => x.Id ==  deletedReferenceId);
                    existingOkrRule.References.Remove(itemToRemove);
                }

                await ApplicationDbContext.SaveChangesAsync();

                var addedReferenceIds = newReferenceIds.Except(existingReferenceIds);
                foreach (var addedReferenceId in addedReferenceIds)
                {
                    var reference = await ApplicationDbContext.ReferenceSources
                        .Where(x => x.Id == addedReferenceId)
                        .FirstOrDefaultAsync();

                    if (reference != null)
                    {
                        existingOkrRule.References.Add(reference);
                    }
                }

                await ApplicationDbContext.SaveChangesAsync();

                await transaction.CommitAsync();

                return existingOkrRule;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }



        /// <summary>
        /// Update the active status of an OKR rule.
        /// </summary>
        /// <param name="okrRuleId"></param>
        /// <param name="activeStatus"></param>
        /// <returns></returns>
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
