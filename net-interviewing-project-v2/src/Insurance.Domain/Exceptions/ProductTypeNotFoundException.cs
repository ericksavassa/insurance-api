namespace Insurance.Domain.Exceptions
{
    public sealed class ProductTypeNotFoundException : NotFoundException
    {
        public ProductTypeNotFoundException(int productTypeId)
            : base($"The product type with the identifier {productTypeId} was not found.") { }
    }
}