namespace DemoAPI.Models.Data
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public int User_id { get; set; }
        public int Category_id { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public int Created_by { get; set; }
        public int Updated_by { get; set; }
    }
}
