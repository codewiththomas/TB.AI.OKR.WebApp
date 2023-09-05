using Microsoft.EntityFrameworkCore;
using TB.AI.OKR.WebApp.Dtos;
using TB.AI.OKR.WebApp.Persistence.Contexts;
using TB.AI.OKR.WebApp.Persistence.Entities;

namespace TB.AI.OKR.WebApp.Persistence.Repositories;

public class OkrRepository : IOkrRepository
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="applicationDbContext"></param>
    public OkrRepository(ApplicationDbContext applicationDbContext)
    {
        ApplicationDbContext = applicationDbContext;
    }

    private ApplicationDbContext ApplicationDbContext { get; }

    public async Task<Okr> AddAsync(Okr okr)
    {
        if (okr.Id != 0)
        {
            throw new Exception("Please use update method for existing okr.");
        }

        await ApplicationDbContext.AddAsync(okr);
        var result = await ApplicationDbContext.SaveChangesAsync();

        return okr;
    }

    /// <summary>
    /// Get a list of all OKR sets.
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<Okr>> GetAllAsync()
    {
        var okrs = await ApplicationDbContext.Okrs
            .Include(kr => kr.KeyResults)
            .ToListAsync();

        return okrs;
    }


    public async Task<Okr?> GetByIdAsync(int id)
    {
        var okr = await ApplicationDbContext.Okrs
            .Include(kr => kr.KeyResults)
            .FirstOrDefaultAsync(x => x.Id == id);

        return okr;
    }

    public async Task<Okr?> UpdateAsync(UpdateOkrDto updateOkrDto)
    {
        if (updateOkrDto.Id == 0)
        {
            throw new Exception("No valid id (0) provided for update.");
        }

        var existingOkr = await ApplicationDbContext.Okrs
            .FirstOrDefaultAsync(x => x.Id == updateOkrDto.Id);

        if (existingOkr == null)
        {
            throw new Exception($"No okr found for id {updateOkrDto.Id}!");
        }
        
        existingOkr.Language = updateOkrDto.Language ?? existingOkr.Language;
        existingOkr.IsCompleteSet = updateOkrDto.IsCompleteSet ?? existingOkr.IsCompleteSet;
        existingOkr.Objective = updateOkrDto.Objective ?? existingOkr.Objective;

        await ApplicationDbContext.SaveChangesAsync();

        return existingOkr;
    }
}
