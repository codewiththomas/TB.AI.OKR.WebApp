namespace TB.AI.OKR.WebApp.Core.Domain.Abstractions;

/// <summary>
/// Abstract parent class for entities with an Id.
/// </summary>
public abstract class BaseEntity
{
    public int Id { get; set; }
}
