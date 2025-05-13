namespace InventoryAPI.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SKU { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string? Barcode { get; set; }
    }
}
