namespace WebApplication15.Models
{
    public class Order
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public double TotalAmount { get; set; }

        //public User User { get; set; }

        public List<OrderItem> OrderItems { get; set; }
    }
     public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        //public Order Order { get; set; }
        public long BookId { get; set; }
        public Book Book { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
    }
}
