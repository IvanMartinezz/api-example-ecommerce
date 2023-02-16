namespace DemoAPI.Models.Data
{
    public class Order
    {
        public int Id { get; set; }
        public float Total { get; set; }
        public int User_id { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
