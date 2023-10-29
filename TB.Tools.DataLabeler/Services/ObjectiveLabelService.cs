using Microsoft.Extensions.Configuration;
using TB.Tools.DataLabeler.Services.Abstract;

namespace TB.Tools.DataLabeler.Services;

public class ObjectiveLabelService : OkrSetElementLabelService
{
    public ObjectiveLabelService(IConfiguration configuration, LabelProviders labelProvider) 
        : base(configuration, labelProvider, "objective")
    {
    }
}
