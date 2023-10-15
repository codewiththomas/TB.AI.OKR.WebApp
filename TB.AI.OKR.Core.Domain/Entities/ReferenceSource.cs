using System.ComponentModel.DataAnnotations;
using TB.AI.OKR.WebApp.Core.Domain.Abstractions;

namespace TB.AI.OKR.Core.Domain;

public class ReferenceSource : BaseEntity
{
    [Required]
    public string ReferenceSymbol { get; set; } = string.Empty;

    public string? ReferenceText { get; set; }

    public IList<OkrSet>? OkrSets { get; set; }

    public IList<OkrRule>? OkrRules { get; set; }
}
