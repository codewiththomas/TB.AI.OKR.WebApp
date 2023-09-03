using System.ComponentModel.DataAnnotations;

namespace TB.AI.OKR.WebApp.Persistence.Entities
{
    public class KeyResult : BaseEntity
    {
        [Required]
        public string Text { get; set; } = string.Empty;
    }
}
