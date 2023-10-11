using System.Text;
using TB.AI.OKR.WebApp.Persistence.Entities;

namespace TB.AI.OKR.WebApp.Dtos;

public class OkrSetDto
{
    public string Objective { get; set; } = string.Empty;

    public IList<OkrSetElement> KeyResults { get; set; } = new List<OkrSetElement>
    {
        new OkrSetElement { Type = "keyresult" },
        new OkrSetElement { Type = "keyresult" },
        new OkrSetElement { Type = "keyresult" }
    };



    public override string ToString()
    {
        var result = new StringBuilder();

        result.AppendLine($"Objective: {Objective}.");
       
        for (int i = 0; i < KeyResults.Count(); i++)
        {
            if (string.IsNullOrWhiteSpace(KeyResults[i].Text))
            {
                continue;
            }
            result.AppendLine($"Key Result {i + 1}: {KeyResults[i]}.");
        }

        return result.ToString();
    }
}
