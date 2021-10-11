using Insurance.Application.Services;
using Insurance.Domain.Dtos;
using Insurance.Domain.Entities;
using Insurance.Domain.Interfaces.Repositories;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Insurance.Tests.Application
{
    public class SurchargeTest
    {
        [Fact]
        public async Task AddSurchargeRate_FoundSurcharge_ShouldUpdate()
        {
            //arrange
            var productTypeId = 1;
            var repository = new Mock<ISurchargeRepository>();
            var surcharge = BuildSurcharge();
            var productTypesSurchargeList = new List<ProductTypeSurchargeDto>()
            {
                new ProductTypeSurchargeDto() { ProductTypeId = productTypeId, SurchargeRate = 30 }
            };

            repository.Setup(p => p.GetByProductTypeId(productTypeId)).ReturnsAsync(surcharge);
            var service = new SurchargeService(repository.Object);

            //act
            await service.AddSurchargeRatesForProductTypes(productTypesSurchargeList);

            //assert
            repository.Verify(p => p.GetByProductTypeId(productTypeId), Times.Once);
            repository.Verify(p => p.Update(It.IsAny<Surcharge>()), Times.Once);
        }

        [Fact]
        public async Task AddSurchargeRate_NotFoundSurcharge_ShouldCreate()
        {
            //arrange
            var productTypeId = 1;
            var repository = new Mock<ISurchargeRepository>();
            Surcharge nullSurcharge = null;

            var productTypesSurchargeList = new List<ProductTypeSurchargeDto>()
            {
                new ProductTypeSurchargeDto() { ProductTypeId = productTypeId, SurchargeRate = 30 }
            };

            repository.Setup(p => p.GetByProductTypeId(productTypeId)).ReturnsAsync(nullSurcharge);
            var service = new SurchargeService(repository.Object);

            //act
            await service.AddSurchargeRatesForProductTypes(productTypesSurchargeList);

            //assert
            repository.Verify(p => p.GetByProductTypeId(productTypeId), Times.Once);
            repository.Verify(p => p.Create(It.IsAny<Surcharge>()), Times.Once);
        }

        private Surcharge BuildSurcharge()
        {
            return new Surcharge(1, 20);
        }
    }
}
