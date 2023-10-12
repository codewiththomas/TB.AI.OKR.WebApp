using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

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


    public string ToString(bool includePrefix = true, int? index = null)
    {
        var result = new StringBuilder();

        if (includePrefix)
        {
            var prefix = Type switch
            {
                "objective" => "Objective",
                "keyresult" => "Key Result",
                _ => Type.Length > 0 ? (char.ToUpper(Type[0]) + Type.Substring(1)) : Type.ToString()
            };
            result.Append(prefix);
            if (index != null)
            {
                result.Append(index);
            }
            result.Append(": ");
        }

        result.Append(Text);

        return result.ToString();
    }

}
