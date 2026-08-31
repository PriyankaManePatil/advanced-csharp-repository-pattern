using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

/// <summary>
/// Input contract shared by create and update operations. Data annotations describe basic request shape;
/// ProductService repeats important use-case validation so the rule remains true outside HTTP callers.
/// </summary>
public sealed class SaveProductRequest
{
    /// <summary>Required product name, limited to the same maximum configured in EF Core.</summary>
    [Required, StringLength(200)]
    public string Name { get; init; } = string.Empty;

    /// <summary>Non-negative price. Decimal avoids binary floating-point rounding for money-like values.</summary>
    [Range(typeof(decimal), "0", "79228162514264337593543950335")]
    public decimal Price { get; init; }
}
