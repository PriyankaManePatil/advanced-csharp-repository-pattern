namespace Application.DTOs;

/// <summary>
/// Read model returned to API clients. A DTO prevents persistence-specific entity details from becoming part
/// of the public contract and allows the API shape to evolve independently from EF Core mappings.
/// </summary>
/// <param name="ProductId">Stable identifier exposed by the API.</param>
/// <param name="Name">Normalised product name.</param>
/// <param name="Price">Current product price.</param>
public sealed record ProductDto(int ProductId, string Name, decimal Price);
