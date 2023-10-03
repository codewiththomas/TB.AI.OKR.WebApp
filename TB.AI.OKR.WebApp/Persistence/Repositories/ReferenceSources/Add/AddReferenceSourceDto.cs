using System.ComponentModel.DataAnnotations;

namespace TB.AI.OKR.WebApp.Persistence.Repositories;

public class AddReferenceSourceDto
{
    [Required]
    public string ReferenceSymbol { get; set; } = string.Empty;

    [Required]
    public string ReferenceText { get; set; } = string.Empty;
}
