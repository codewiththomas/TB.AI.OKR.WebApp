using System.ComponentModel.DataAnnotations;
using System.Text;
using TB.AI.OKR.WebApp.Core.Domain.Abstractions;

namespace TB.AI.OKR.Core.Domain;

public class OkrRule : BaseEntity
{
    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = string.Empty;

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
        => ToString(true);


    /// <summary>
    /// Returns Description with severity as prefix. No prefix added when severity info.
    /// </summary>
    /// <param name="includeScope">Indicates whether the sentence should start with the Scope, like "An Objective.."</param>
    /// <returns></returns>
    public string ToString(bool includeScope)
    {
        var result = new StringBuilder();
        
        if (includeScope)
        {
            result.Append("A");

            char firstLetterOfScope = Scope.ToString()[0];
            string vowels = "aeiou";

            if (vowels.Contains(firstLetterOfScope, StringComparison.OrdinalIgnoreCase))
            {
                result.Append("n");
            }

            result.Append(" " + Scope.ToString());
        }

        switch (Severity)
        {

            case OkrRuleSeverities.Should:
                result.Append(" should");
                break;
            case OkrRuleSeverities.Must:
                result.Append(" must");
                break;
            case OkrRuleSeverities.Info:
            default:
                break;
        }

        return result.Append(" " + Description).ToString();
    }
}
