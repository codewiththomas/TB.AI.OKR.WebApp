using System.ComponentModel.DataAnnotations.Schema;

namespace TB.AI.OKR.WebApp.Persistence.Entities;

/// <summary>
/// Labels for supervised machine learning.
/// </summary>
/// <typeparam name="T"></typeparam>
public class Label<T> : BaseEntity
{
    public int EntityId { get; set; }
    [ForeignKey(nameof(EntityId))]
    public T? Entity { get; set; }
       

    public string Title { get; set; }

    public string Value { get; set; }
}
