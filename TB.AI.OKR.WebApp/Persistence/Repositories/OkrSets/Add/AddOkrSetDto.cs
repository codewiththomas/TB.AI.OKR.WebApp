namespace TB.AI.OKR.WebApp.Persistence.Repositories;

public class AddOkrSetDto
{
    public string? Vision { get; set; }

    public string? Level { get; set; }

    public string Language { get; set; } = "en";

    public string Objective { get; set; } = string.Empty;

    public IList<int> ReferenceSourceIds { get; set; }
        = new List<int>();

    public IList<string> KeyResults { get; set; }
        = new List<string>();
}
