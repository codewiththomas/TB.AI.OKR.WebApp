using Microsoft.EntityFrameworkCore;
using TB.AI.OKR.WebApp.Persistence.Contexts;
using TB.AI.OKR.WebApp.Persistence.Entities;
using TB.AI.OKR.WebApp.Persistence.Repositories.Labels;

namespace TB.AI.OKR.WebApp.Persistence.Repositories;

public class LabelRepository : ILabelRepository
{
    /// <summary>
    /// Constructor for dependency injection. Creates a new instance of LabelsRepository.
    /// </summary>
    /// <param name="applicationDbContext"></param>
    public LabelRepository(ApplicationDbContext applicationDbContext)
    {
        ApplicationDbContext = applicationDbContext;
    }


    public ApplicationDbContext ApplicationDbContext { get; }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public async Task<IList<Label<OkrSet>>> GetOkrSetLabelsAsync(LabelsFilter? filter)
    {
        var labelsQuery = ApplicationDbContext.OkrSetLabels
            .AsQueryable();

        if (filter is not null)
        {
            if (filter.EntityIds is not null && filter.EntityIds.Any())
            {
                labelsQuery = labelsQuery
                    .Where(x => filter.EntityIds.Contains(x.EntityId))
                    .AsQueryable();
            }

            if (filter.LabelProvider is not null)
            {
                labelsQuery = labelsQuery
                    .Where(x => x.LabelProvider.Equals(filter.LabelProvider))
                    .AsQueryable();
            }

            if (filter.LabelName is not null)
            {
                labelsQuery = labelsQuery
                    .Where(x => x.LabelName.Equals(filter.LabelName))
                    .AsQueryable();
            }
        }

        var labels = await labelsQuery.ToListAsync();

        return labels;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public async Task<IList<Label<OkrSetElement>>> GetOkrSetElementLabelsAsync(LabelsFilter filter)
    {
        var labelsQuery = ApplicationDbContext.OkrSetElementLabels
            .AsQueryable();

        if (filter is not null)
        {
            if (filter.EntityIds is not null && filter.EntityIds.Any())
            {
                labelsQuery = labelsQuery
                    .Where(x => filter.EntityIds.Contains(x.EntityId))
                    .AsQueryable();
            }

            if (filter.LabelProvider is not null)
            {
                labelsQuery = labelsQuery
                    .Where(x => x.LabelProvider.Equals(filter.LabelProvider))
                    .AsQueryable();
            }

            if (filter.LabelName is not null)
            {
                labelsQuery = labelsQuery
                    .Where(x => x.LabelName.Equals(filter.LabelName))
                    .AsQueryable();
            }
        }

        var labels = await labelsQuery.ToListAsync();

        return labels;
    }

}
