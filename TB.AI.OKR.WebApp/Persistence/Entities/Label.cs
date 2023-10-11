using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TB.AI.OKR.WebApp.Persistence.Entities;

/// <summary>
/// Labels for supervised machine learning.
/// </summary>
/// <typeparam name="T"></typeparam>
[PrimaryKey(nameof(EntityId), nameof(LabelProvider), nameof(LabelName))]
public class Label<T>
{
    public int EntityId { get; set; }
    [ForeignKey(nameof(EntityId))]
    public T? Entity { get; set; }

    public string LabelProvider { get; set; } = "base";

    public string LabelName { get; set; } = string.Empty;

    public string Value { get; set; } = string.Empty;

    public string ValueType { get; set; } = "string";

    public string? Comment { get; set; }

    public TimeSpan LabelingDuration { get; set; }
}
