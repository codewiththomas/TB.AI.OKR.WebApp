using System.Text;
using TB.AI.OKR.WebApp.Persistence.Entities;

namespace TB.AI.OKR.WebApp.Extensions;

public static class OkrSetExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="okrSet"></param>
    /// <param name="delimiter"></param>
    /// <param name="delimiterAfterLastElement"></param>
    /// <returns></returns>
    public static string ToSetString(this OkrSet okrSet, string delimiter = "\n", bool delimiterAfterLastElement = true)
    {
        var resultString = new StringBuilder();

        var objective = okrSet.OkrSetElements?
            .FirstOrDefault(x => x.Type.Equals("objective", StringComparison.OrdinalIgnoreCase))?
            .Text ?? string.Empty;

        resultString.Append($"Objective: {objective}");

        var keyResults = okrSet.OkrSetElements?
            .Where(x => x.Type.Equals("keyresult", StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (keyResults is not null &&  keyResults.Any())
        {
            for (int i = 0; i < keyResults.Count; i++) 
            {
                resultString.Append($"{delimiter}Key Result {(i + 1)}: {keyResults[i].Text}");
            }
        }

        if (delimiterAfterLastElement)
        {
            resultString.Append(delimiter);
        }

        return resultString.ToString();
    }
}
