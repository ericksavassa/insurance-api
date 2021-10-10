using Insurance.Domain.Entities;
using System.Threading.Tasks;

namespace Insurance.Domain.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GetProduct(int productId);

        Task<ProductType> GetProductType(int productId);
    }
}