using Insurance.Domain.Entities;
using System.Threading.Tasks;

namespace Insurance.Domain.Interfaces.Repositories
{
    public interface ISurchargeRepository
    {
        Task Create(Surcharge surcharge);
        Task<bool> Update(Surcharge surcharge);
        Task<Surcharge> GetByProductTypeId(int productTypeId);
    }
}