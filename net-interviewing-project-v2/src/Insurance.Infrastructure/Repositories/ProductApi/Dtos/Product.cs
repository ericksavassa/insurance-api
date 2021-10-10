namespace Insurance.Infrastructure.ProductApi.Dtos
{
    public class Product
    {
        public int Id { get; set; }
        public int ProductTypeId { get; set; }
        public string Name { get; set; }
        public float SalesPrice { get; set; }
    }
}