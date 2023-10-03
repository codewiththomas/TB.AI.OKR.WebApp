using System.ComponentModel.DataAnnotations;

namespace TB.AI.OKR.WebApp.Persistence.Entities
{
    public class ReferenceSource : BaseEntity
    {
        [Required]
        public string ReferenceSymbol { get; set; } = string.Empty;

        public string? ReferenceText { get; set; }

        public IList<OkrSet>? OkrSets { get; set; }

        public IList<OkrRule>? OkrRules { get; set; }
    }
}
