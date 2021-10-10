using System.Threading.Tasks;

namespace Insurance.Domain.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<Entities.Product> GetProduct(int productId);

        Task<Entities.ProductType> GetProductType(int productId);
    }
}