using Insurance.Domain.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Insurance.Application.Services.Interfaces
{
    public interface ISurchargeService
    {
        Task AddSurchargeRatesForProductTypes(IList<ProductTypeSurchargeDto> productTypesSurchargeList);
    }
}