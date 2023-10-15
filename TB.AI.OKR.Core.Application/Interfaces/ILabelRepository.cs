using TB.AI.OKR.Core.Domain;

namespace TB.AI.OKR.Core.Application;

public interface ILabelRepository
{
    Task<IList<Label<OkrSet>>> GetOkrSetLabelsAsync(LabelsFilter? filter);

    Task<IList<Label<OkrSetElement>>> GetOkrSetElementLabelsAsync(LabelsFilter filter);
}
