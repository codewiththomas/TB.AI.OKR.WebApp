﻿namespace TB.AI.OKR.Core.Application;

public class GetOkrSetsDto
{
    public int Id { get; set; }

    public string Language { get; set; } 
        = string.Empty;

    public string? Vision { get; set; }    
    public bool HasVision 
        => !string.IsNullOrWhiteSpace(Vision);

    public string? Level { get; set; }

    public string Objective { get; set; } = string.Empty;

    public IEnumerable<string> KeyResults { get; set; } 
        = new List<string>();

    public IEnumerable<GetReferenceSourcesDto> References { get; set; }
        = new List<GetReferenceSourcesDto>();

    public bool IsTrainingData { get; set; }
}

