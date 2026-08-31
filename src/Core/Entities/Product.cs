namespace Core.Entities;

/// <summary>Represents a product in the catalogue.</summary>
public sealed class Product
{
    public int ProductId { get; set; }
    public required string Name { get; set; }
    public decimal Price { get; set; }
}
