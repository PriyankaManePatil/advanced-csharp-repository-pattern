namespace Core.Entities;

/// <summary>Represents a product in the catalogue.</summary>
public sealed class Product : IEntity
{
    /// <summary>The database-generated key used by generic repository queries.</summary>
    public int Id { get; set; }

    /// <summary>Domain-friendly alias retained for API readability; EF Core ignores this alias.</summary>
    [System.ComponentModel.DataAnnotations.Schema.NotMapped]
    public int ProductId { get => Id; set => Id = value; }

    /// <summary>The customer-facing product name.</summary>
    public required string Name { get; set; }

    /// <summary>The current unit price; validation occurs in the application layer.</summary>
    public decimal Price { get; set; }
}
