using Microsoft.Extensions.Configuration;
using TB.Tools.DataLabeler.Services;

namespace TB.Tools.DataLabeler;

public class Labeler
{
    public OkrSetLabelService OkrSetLabeler { get; set; }

    public ObjectiveLabelService ObjectiveLabeler { get; set; }
    public KeyResultLabelService KeyResultLabeler { get; set; }

    public Labeler(IConfiguration configuration, LabelProviders labelProvider)
    {
        OkrSetLabeler = new OkrSetLabelService(configuration, labelProvider);
        ObjectiveLabeler = new ObjectiveLabelService(configuration, labelProvider);
        KeyResultLabeler = new KeyResultLabelService(configuration, labelProvider);
    }
}
