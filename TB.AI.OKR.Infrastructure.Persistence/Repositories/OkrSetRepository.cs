using Microsoft.EntityFrameworkCore;
using TB.AI.OKR.Core.Application;
using TB.AI.OKR.Core.Domain;
using TB.AI.OKR.Infrastructure.Persistence.Contexts;

namespace TB.AI.OKR.Infrastructure.Persistence;

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
                Vision = addOkrSetDto.Vision,
                UseForSampleDataset = addOkrSetDto.UseForSampleDataset
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
        catch (Exception)
        {
            await transaction.RollbackAsync();            
            throw;
        }
    }


    /// <summary>
    /// Updates an existing OKR set.
    /// </summary>
    /// <param name="updateOkrSetDto"></param>
    /// <returns></returns>
    public async Task<OkrSet> UpdateAsync(UpdateOkrSetDto updateOkrSetDto)
    {
        var existingOkrSet = await ApplicationDbContext.OkrSets
            .Include(x => x.OkrSetElements)
            .Include(x => x.References)
            .FirstOrDefaultAsync(x => x.Id == updateOkrSetDto.Id);

        if (existingOkrSet is null)
        {
            throw new Exception("OKR set not found");
        }

        using var transaction = await ApplicationDbContext.Database.BeginTransactionAsync();

        try
        {
            #region Update basic set

            existingOkrSet.AuthorsRating = updateOkrSetDto.AuthorsRating;
            existingOkrSet.Comment = string.IsNullOrWhiteSpace(updateOkrSetDto.Comment) ? null : updateOkrSetDto.Comment;
            existingOkrSet.Language = updateOkrSetDto.Language;
            existingOkrSet.Level = string.IsNullOrWhiteSpace(updateOkrSetDto.Level) ? null : updateOkrSetDto.Level;
            existingOkrSet.Vision = string.IsNullOrWhiteSpace(updateOkrSetDto.Vision) ? null : updateOkrSetDto.Vision;
            existingOkrSet.UseForSampleDataset = updateOkrSetDto.UseForSampleDataset;

            await ApplicationDbContext.SaveChangesAsync();

            #endregion

            #region Update objective

            var objective = existingOkrSet.OkrSetElements
                .FirstOrDefault(x => x.Type.ToLower() == "objective");

            if (objective is null)
            {
                //add new objective
                var newObjective = new OkrSetElement
                {
                    Type = "objective",
                    Text = updateOkrSetDto.Objective,
                    OkrSetId = updateOkrSetDto.Id
                };
                await ApplicationDbContext.AddAsync(newObjective);
            }
            else
            {
                objective.Text = updateOkrSetDto.Objective;
            }
            await ApplicationDbContext.SaveChangesAsync();

            #endregion

            #region update key results

            var existingKeyResultCount = existingOkrSet.OkrSetElements
                .Where(x => x.Type.ToLower() == "keyresult")
                .Count();

            var newKeyResultCount = updateOkrSetDto.KeyResults
                .Count();

            var maxKeyResultsCount = Math.Max(existingKeyResultCount, newKeyResultCount);

            for (int i = 0; i < maxKeyResultsCount; i++)
            {
                if (i < existingKeyResultCount)
                {
                    var existingKeyResult = existingOkrSet.OkrSetElements
                        .Where(x => x.Type.ToLower() == "keyresult")
                        .ToArray()[i];

                    if (i < newKeyResultCount)
                    {
                        //existing key result must be updated
                        var newKeyResult = updateOkrSetDto.KeyResults[i];
                        existingKeyResult.Text = newKeyResult;
                    }
                    else
                    {
                        //existing key result must be deleted
                        existingOkrSet.OkrSetElements.Remove(existingKeyResult);
                    }
                }
                else //new KeyResult must be added
                {
                    var newKeyResult = new OkrSetElement
                    {
                        Type = "keyresult",
                        Text = updateOkrSetDto.KeyResults[i],
                        OkrSetId = updateOkrSetDto.Id
                    };
                    existingOkrSet.OkrSetElements.Add(newKeyResult);
                }
            }
            await ApplicationDbContext.SaveChangesAsync();

            var emptyKeyResults = existingOkrSet.OkrSetElements
                .Where(x => x.Type.ToLower() == "keyresult" && string.IsNullOrWhiteSpace(x.Text))
                .ToList();

            foreach (var emptyKeyResult in emptyKeyResults)
            {
                existingOkrSet.OkrSetElements.Remove(emptyKeyResult);
            }

            await ApplicationDbContext.SaveChangesAsync();

            #endregion

            #region Update references

            var existingReferenceIds = existingOkrSet.References.Select(x => x.Id).ToList();
            var newReferenceIds = updateOkrSetDto.ReferenceSourceIds;

            var deletedReferenceIds = existingReferenceIds.Except(newReferenceIds);
            foreach (var deletedReferenceId in deletedReferenceIds)
            {
                var itemToRemove = existingOkrSet.References.First(x => x.Id == deletedReferenceId);
                existingOkrSet.References.Remove(itemToRemove);
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
                    existingOkrSet.References.Add(reference);
                }
            }

            await ApplicationDbContext.SaveChangesAsync();

            #endregion

            await transaction.CommitAsync();

            return existingOkrSet;
        }
        catch (Exception)
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
