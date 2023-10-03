namespace TB.AI.OKR.WebApp.Persistence.Repositories;

public class UpdateReferenceSourceDto
{
    public int Id { get; set; }

    public string? ReferenceSymbol { get; set; } = string.Empty;

    public string? ReferenceText { get; set; }
}
