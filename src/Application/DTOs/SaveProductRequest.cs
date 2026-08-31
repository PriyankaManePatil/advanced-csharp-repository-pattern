using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

public sealed class SaveProductRequest
{
    [Required, StringLength(200)]
    public string Name { get; init; } = string.Empty;

    [Range(typeof(decimal), "0", "79228162514264337593543950335")]
    public decimal Price { get; init; }
}
