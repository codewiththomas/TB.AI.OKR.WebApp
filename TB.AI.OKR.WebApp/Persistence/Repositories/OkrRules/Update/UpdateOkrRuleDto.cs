using System.ComponentModel.DataAnnotations;

namespace TB.AI.OKR.WebApp.Persistence.Repositories;

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
