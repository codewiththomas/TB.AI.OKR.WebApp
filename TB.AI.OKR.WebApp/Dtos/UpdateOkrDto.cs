namespace TB.AI.OKR.WebApp.Dtos;

public class UpdateOkrDto
{
    public int Id { get; set; }

    public string? Objective { get; set; }

    public string? Language { get; set; }

    public bool? IsCompleteSet { get; set; }
}
