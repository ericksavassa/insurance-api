namespace Insurance.Domain.Exceptions
{
    public sealed class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException(int productId)
            : base($"The product with the identifier {productId} was not found.") { }
    }
}