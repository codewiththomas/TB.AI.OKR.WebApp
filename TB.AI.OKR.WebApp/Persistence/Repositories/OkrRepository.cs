using Microsoft.EntityFrameworkCore;
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

}
