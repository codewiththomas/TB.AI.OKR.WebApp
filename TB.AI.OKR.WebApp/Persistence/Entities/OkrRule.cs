using System.ComponentModel.DataAnnotations;

namespace TB.AI.OKR.WebApp.Persistence.Entities;

public class OkrRule : BaseEntity
{
    [Required]
    [MaxLength(4096)]
    public string Description { get; set; } = string.Empty;

    public bool IsActive { get; set; }

    public OkrRuleScopes Scope { get; set; }

    public OkrRuleSeverities Severity { get; set; }

    public IList<ReferenceSource> References { get; set; } = new List<ReferenceSource>();

    /// <summary>
    /// Returns Description with severity as prefix. No prefix added when severity info.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        if (Severity == OkrRuleSeverities.Info)
        {
            return Description;
        }

        switch (Severity)
        {
            case OkrRuleSeverities.Should:
                return "should " + Description;
            case OkrRuleSeverities.Must:
                return "must " + Description;
            default:
                return Description;
        }
    }
}
