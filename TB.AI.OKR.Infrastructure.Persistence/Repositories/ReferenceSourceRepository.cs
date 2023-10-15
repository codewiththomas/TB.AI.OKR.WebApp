using Microsoft.EntityFrameworkCore;
using TB.AI.OKR.Core.Application;
using TB.AI.OKR.Core.Domain;
using TB.AI.OKR.Infrastructure.Persistence.Contexts;

namespace TB.AI.OKR.Infrastructure.Persistence;

public class ReferenceSourceRepository : IReferenceSourceRepository
{
    public ReferenceSourceRepository(ApplicationDbContext applicationDbContext)
    {
        ApplicationDbContext = applicationDbContext;
    }

    private ApplicationDbContext ApplicationDbContext { get; }


    /// <summary>
    /// Adds a new Reference Source to the database.
    /// </summary>
    /// <param name="addReferenceDto"></param>
    /// <returns></returns>
    public async Task<ReferenceSource> AddAsync(AddReferenceSourceDto addReferenceDto)
    {
        var newReferenceSourceEntity = new ReferenceSource
        {
            ReferenceSymbol = addReferenceDto.ReferenceSymbol,
            ReferenceText = addReferenceDto.ReferenceText
        };

        await ApplicationDbContext.ReferenceSources.AddAsync(newReferenceSourceEntity);
        await ApplicationDbContext.SaveChangesAsync();

        return newReferenceSourceEntity;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existingEntity = await ApplicationDbContext.ReferenceSources
            .FirstOrDefaultAsync(x => x.Id == id);

        if (existingEntity is null)
        {
            throw new Exception("Could not find Reference Source to delete.");
        }

        ApplicationDbContext.Remove(existingEntity);
        var deleteResult = await ApplicationDbContext.SaveChangesAsync();

        return deleteResult > 0;
    }


    /// <summary>
    /// Returns a list of all Reference Sources.
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<GetReferenceSourcesDto>> GetAllAsync()
    {
        var referenceSources = await ApplicationDbContext
            .ReferenceSources
            .ToListAsync();

        var result = new List<GetReferenceSourcesDto>();

        foreach (var referenceSouce in referenceSources)
        {
            var resultEntry = new GetReferenceSourcesDto
            {
                Id = referenceSouce.Id,
                ReferenceSymbol = referenceSouce.ReferenceSymbol,
                ReferenceText = referenceSouce.ReferenceText ?? string.Empty,
            };
            result.Add(resultEntry);
        }

        return result;
    }


    /// <summary>
    /// Retrieves a specific Reference Source by its id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<ReferenceSource?> GetByIdAsync(int id)
    {
        var referenceSource = await ApplicationDbContext.ReferenceSources
            .FirstOrDefaultAsync(x => x.Id == id);

        if (referenceSource is null)
        {
            throw new Exception("Reference Source not found.");
        }

        return referenceSource;
    }


    /// <summary>
    /// Updates an existing Reference Source.
    /// </summary>
    /// <param name="updateRefernceSourceDto"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<ReferenceSource> UpdateAsync(UpdateReferenceSourceDto updateRefernceSourceDto)
    {
        var existingReferenceSource = await ApplicationDbContext.ReferenceSources
            .FirstOrDefaultAsync(x => x.Id == updateRefernceSourceDto.Id);

        if (existingReferenceSource is null)
        {
            throw new Exception("Reference Source not found.");
        }

        if (!string.IsNullOrWhiteSpace(updateRefernceSourceDto.ReferenceSymbol))
        {
            existingReferenceSource.ReferenceSymbol = updateRefernceSourceDto.ReferenceSymbol;
        }

        if (!string.IsNullOrWhiteSpace(updateRefernceSourceDto.ReferenceText))
        {
            existingReferenceSource.ReferenceText = updateRefernceSourceDto.ReferenceText;
        }

        await ApplicationDbContext.SaveChangesAsync();
        return existingReferenceSource;
    }
}
