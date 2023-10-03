using System.ComponentModel.DataAnnotations;

namespace TB.AI.OKR.WebApp.Persistence.Entities;

public class OkrSetElement : BaseEntity
{
    [Required]
    [MaxLength(100)]
    [MinLength(5)]
    public string Type { get; set; } = string.Empty;

    [Required]
    [MaxLength(1024)]
    public string Text { get; set; } = string.Empty;

    [Required]
    public int OkrSetId { get; set; }
    public OkrSet? OkrSet { get; set; }

}
