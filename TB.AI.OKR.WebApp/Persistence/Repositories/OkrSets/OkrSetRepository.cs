using Microsoft.EntityFrameworkCore;
using TB.AI.OKR.WebApp.Persistence.Contexts;
using TB.AI.OKR.WebApp.Persistence.Entities;

namespace TB.AI.OKR.WebApp.Persistence.Repositories;

public class OkrSetRepository : IOkrSetRepository
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="applicationDbContext"></param>
    public OkrSetRepository(ApplicationDbContext applicationDbContext)
    {
        ApplicationDbContext = applicationDbContext;
    }

    private ApplicationDbContext ApplicationDbContext { get; }


    /// <summary>
    /// Adds a new OKR set to the database.
    /// </summary>
    /// <param name="okr"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<OkrSet> AddAsync(AddOkrSetDto addOkrSetDto)
    {
        using var transaction = await ApplicationDbContext.Database.BeginTransactionAsync();

        try
        {
            /* add okr set */
            var newOkrSetEntity = new OkrSet
            {
                AuthorsRating = addOkrSetDto.AuthorsRating,
                Comment = addOkrSetDto.Comment,
                Language = addOkrSetDto.Language,
                Level = addOkrSetDto.Level,
                Vision = addOkrSetDto.Vision                
            };
            await ApplicationDbContext.AddAsync(newOkrSetEntity);
            var result = await ApplicationDbContext.SaveChangesAsync();

            /* add objective */
            var newObjective = new OkrSetElement
            {
                Type = "objective",
                Text = addOkrSetDto.Objective,
                OkrSetId = newOkrSetEntity.Id
            };
            await ApplicationDbContext.AddAsync(newObjective);
            await ApplicationDbContext.SaveChangesAsync();

            /* add key results */
            foreach (var keyResult in addOkrSetDto.KeyResults)
            {
                var newKeyResult = new OkrSetElement
                {
                    Type = "keyresult",
                    Text = keyResult,
                    OkrSetId = newOkrSetEntity.Id
                };
                await ApplicationDbContext.AddAsync(newKeyResult);
                await ApplicationDbContext.SaveChangesAsync();
            }

            /* add references */
            foreach (var referenceId in addOkrSetDto.ReferenceSourceIds)
            {
                var existingReference = await ApplicationDbContext.ReferenceSources
                    .Include(x => x.OkrSets)
                    .FirstOrDefaultAsync(x => x.Id == referenceId);

                if (existingReference is null)
                {
                    continue;
                }

                if (existingReference.OkrSets is null)
                {
                    existingReference.OkrSets = new List<OkrSet>();
                }

                existingReference!.OkrSets.Add(newOkrSetEntity);
                await ApplicationDbContext.SaveChangesAsync();
            }

            await transaction.CommitAsync();

            return newOkrSetEntity;
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();            
            throw;
        }
    }


    /// <summary>
    /// Get a list of all OKR sets.
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<GetOkrSetsDto>> GetAllAsync()
    {
        var okrs = await ApplicationDbContext.OkrSets
            .Include(x => x.OkrSetElements)
            .Include(x => x.References)
            .ToListAsync();

        var result = new List<GetOkrSetsDto>();

        foreach (var okrSet in okrs)
        {
            var resultEntry = new GetOkrSetsDto
            {
                Id = okrSet.Id,
                Language = okrSet.Language,
                Objective = okrSet.OkrSetElements.FirstOrDefault(x => x.Type.ToLower() == "objective")?.Text ?? string.Empty,
                KeyResults = okrSet.OkrSetElements.Where(x => x.Type.ToLower() == "keyresult").Select(x => x.Text).ToList(),
                Vision = okrSet.Vision,
                Level = okrSet.Level,
                References = okrSet.References.Select(x => new GetReferenceSourcesDto
                {
                    Id = x.Id,
                    ReferenceSymbol = x.ReferenceSymbol,
                    ReferenceText = x.ReferenceText ?? string.Empty,
                }).ToList()
            };
            result.Add(resultEntry);
        }

        return result;
    }


    /// <summary>
    /// Retrieves a specific OKR set by its id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<OkrSet?> GetByIdAsync(int id)
    {
        var okr = await ApplicationDbContext.OkrSets
            .Include(x => x.OkrSetElements)
            .FirstOrDefaultAsync(x => x.Id == id);

        return okr;
    }

}
