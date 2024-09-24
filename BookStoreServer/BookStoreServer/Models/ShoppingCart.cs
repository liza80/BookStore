namespace WebApplication15.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public List<CartItem> Items { get; set; } = new List<CartItem>();
    }

    public class CartItem
    {
        public int Id { get; set; }
        public long BookId { get; set; }
        public Book Book { get; set; }
        public int Quantity { get; set; }
    }
}
