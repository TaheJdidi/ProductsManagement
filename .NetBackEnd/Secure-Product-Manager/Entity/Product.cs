namespace Secure_Product_Manager.Entity
{
    public class Product
    {
        public required string name { get; set; }
        public int price { get; set; }
        public int quantity { get; set; }
        public string? image { get; set; }
    }
}
