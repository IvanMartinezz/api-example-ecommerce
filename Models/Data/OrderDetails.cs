namespace DemoAPI.Models.Data
{
    public class OrderDetails
    {
        public int Id { get; set; }
        public float Discount { get; set; }
        public float Quantity { get; set; }
        public int Product_id { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
