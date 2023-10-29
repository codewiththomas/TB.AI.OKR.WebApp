using Microsoft.Extensions.Configuration;
using TB.Tools.DataLabeler.Services.Abstract;

namespace TB.Tools.DataLabeler.Services;

public class KeyResultLabelService : OkrSetElementLabelService
{
    /// <summary>
    /// Constructor for dependency injection.
    /// </summary>
    /// <param name="configuration"></param>
    public KeyResultLabelService(IConfiguration configuration, LabelProviders labelProvider) 
        : base(configuration, labelProvider, "keyresult")
    {
    }

}
