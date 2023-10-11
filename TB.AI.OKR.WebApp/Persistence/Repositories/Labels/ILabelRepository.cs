using TB.AI.OKR.WebApp.Persistence.Entities;
using TB.AI.OKR.WebApp.Persistence.Repositories.Labels;

namespace TB.AI.OKR.WebApp.Persistence.Repositories;

public interface ILabelRepository
{
    Task<IList<Label<OkrSet>>> GetOkrSetLabelsAsync(LabelsFilter? filter);

    Task<IList<Label<OkrSetElement>>> GetOkrSetElementLabelsAsync(LabelsFilter filter);
}
