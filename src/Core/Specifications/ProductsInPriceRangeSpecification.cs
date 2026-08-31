using Core.Entities;

namespace Core.Specifications;

/// <summary>Example of a named, reusable business query represented as a specification.</summary>
public sealed class ProductsInPriceRangeSpecification : Specification<Product>
{
    public ProductsInPriceRangeSpecification(decimal minimumPrice, decimal maximumPrice)
    {
        if (minimumPrice < 0 || maximumPrice < minimumPrice)
            throw new ArgumentOutOfRangeException(nameof(minimumPrice), "Provide a valid non-negative price range.");

        Criteria = product => product.Price >= minimumPrice && product.Price <= maximumPrice;
        OrderBy = product => product.Price;
    }
}
