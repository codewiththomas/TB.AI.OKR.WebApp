using System.Text;

namespace TB.AI.OKR.WebApp.Dtos;

public class OkrSetDto
{
    public string Objective { get; set; } = string.Empty;

    private string[] KeyResults { get; set; } = new string[5];

    public string KR1
    {
        get => KeyResults[0];
        set => KeyResults[0] = value;
    }

    public string KR2
    {
        get => KeyResults[1];
        set => KeyResults[1] = value;
    }

    public string KR3
    {
        get => KeyResults[2];
        set => KeyResults[2] = value;
    }

    public string KR4
    {
        get => KeyResults[3];
        set => KeyResults[3] = value;
    }

    public string KR5
    {
        get => KeyResults[4];
        set => KeyResults[4] = value;
    }

    public override string ToString()
    {
        var result = new StringBuilder();

        result.AppendLine($"Objective: {Objective}.");
       
        for (int i = 0; i < KeyResults.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(KeyResults[i]))
            {
                continue;
            }
            result.AppendLine($"Key Result {i + 1}: {KeyResults[i]}.");
        }

        return result.ToString();
    }
}
