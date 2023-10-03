namespace TB.AI.OKR.WebApp.Persistence.Repositories;

public class GetReferenceSourcesDto
{
    public int Id { get; set; }

    public string ReferenceSymbol { get; set; } = string.Empty;

    public string ReferenceText { get; set; } = string.Empty;

    public int CountRules { get; set; }

    public int CountOkrSets { get; set; }
}
