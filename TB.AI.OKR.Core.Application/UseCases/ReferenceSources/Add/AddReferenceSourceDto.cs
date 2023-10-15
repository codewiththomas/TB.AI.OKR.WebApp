using System.ComponentModel.DataAnnotations;

namespace TB.AI.OKR.Core.Application;

public class AddReferenceSourceDto
{
    [Required]
    public string ReferenceSymbol { get; set; } = string.Empty;

    [Required]
    public string ReferenceText { get; set; } = string.Empty;
}
