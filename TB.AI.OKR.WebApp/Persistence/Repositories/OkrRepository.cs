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

    /// <summary>
    /// Get a list of all OKR sets.
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<Objective>> GetAllAsync()
    {
        var okrs = await ApplicationDbContext.Objectives
            .Include(kr => kr.KeyResults)
            .ToListAsync();

        return okrs;
    }

}
