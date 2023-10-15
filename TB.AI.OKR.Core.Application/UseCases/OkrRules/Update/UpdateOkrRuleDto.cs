using System.ComponentModel.DataAnnotations;
using TB.AI.OKR.Core.Domain;

namespace TB.AI.OKR.Core.Application;

public class UpdateOkrRuleDto
{
    public int Id { get; set; }

    [MaxLength(150)]
    [Required]
    public string OkrRuleTitle { get; set; } = string.Empty;

    [Required]
    public string OkrRuleText { get; set; } = string.Empty;

    public OkrRuleScopes Scope { get; set; }

    public OkrRuleSeverities Severity { get; set; }

    public IList<int> ReferenceSourceIds { get; set; }
        = new List<int>();
}
