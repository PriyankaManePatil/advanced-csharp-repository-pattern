namespace Core.Entities
{
    /// <summary>
    /// Represents a product entity in the system.
    /// </summary>
    public class Product
    {
        public int ProductId { get; set; }   // Primary key
        public string Name { get; set; }     // Product name
        public decimal Price { get; set; }   // Product price
    }
}
