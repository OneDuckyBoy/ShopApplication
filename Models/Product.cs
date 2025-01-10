namespace Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Double Price { get; set; }
        public string Aroma { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
    }
}
