using Insurance.Application.Services.Interfaces;
using Insurance.Domain.Dtos;
using Insurance.Domain.Entities;
using Insurance.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Insurance.Application.Services
{
    public class SurchargeService : ISurchargeService
    {
        private readonly ISurchargeRepository _surchargeRepository;

        public SurchargeService(ISurchargeRepository surchargeRepository)
        {
            _surchargeRepository = surchargeRepository;
        }

        public async Task AddSurchargeRatesForProductTypes(IList<ProductTypeSurchargeDto> productTypesSurchargeList)
        {
            foreach (var item in productTypesSurchargeList)
            {
                var surcharge = new Surcharge(item.ProductTypeId, item.SurchargeRate);
                await SaveSurcharge(surcharge);
            };
        }

        private async Task SaveSurcharge(Surcharge surcharge)
        {
            var storedItem = await this._surchargeRepository.GetByProductTypeId(surcharge.ProductTypeId);
            if (storedItem == null)
            {
                await this._surchargeRepository.Create(surcharge);
            }
            else
            {
                surcharge.SetId(storedItem.Id);
                await this._surchargeRepository.Update(surcharge);
            }
        }
    }
}