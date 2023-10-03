namespace TB.AI.OKR.WebApp.Persistence.Repositories;

public class AddOkrSetDto
{
    public string? AuthorsRating { get; set; }

    public string? Comment { get; set; }

    public string Language { get; set; } = "en";

    public string? Level { get; set; }

    public string Objective { get; set; } = string.Empty;

    public string? Vision { get; set; }
    

    public IList<int> ReferenceSourceIds { get; set; }
        = new List<int>();

    public IList<string> KeyResults { get; set; }
        = new List<string>();
}
