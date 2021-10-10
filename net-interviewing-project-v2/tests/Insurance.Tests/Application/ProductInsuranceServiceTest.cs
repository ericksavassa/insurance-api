using Insurance.Application.Services;
using Insurance.Application.Services.Interfaces;
using Insurance.Domain.Entities;
using Insurance.Domain.Interfaces.Repositories;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Insurance.Tests.Application
{
    public class ProductInsuranceServiceTest
    {
        private IProductInsuranceService _productInsuranceService;

        public ProductInsuranceServiceTest()
        {
        }

        [Fact]
        public async Task CalculateProductInsurance_GivenSalesPriceLessThan500_ShouldAdd500ToInsuranceCost()
        {
            //arrange
            const float expectedInsuranceValue = 500;
            const int productId = 1;
            const int productTypeId = 10;
            Product product = BuildProduct(productId, productTypeId, salesPrice: 400);
            ProductType productType = BuildProductType(productTypeId, name: "Type", hasInsurance: true);

            var repository = new Mock<IProductRepository>();
            repository.Setup(p => p.GetProduct(productId)).ReturnsAsync(product);
            repository.Setup(p => p.GetProductType(productTypeId)).ReturnsAsync(productType);

            _productInsuranceService = new ProductInsuranceService(repository.Object);

            //act
            var result = await _productInsuranceService.CalculateProductInsurance(productId);

            //assert
            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result
            );
        }

        [Fact]
        public async Task CalculateProductInsurance_GivenSalesPriceBetween500And2000_ShouldAddThousandToInsuranceCost()
        {
            //arrange
            const float expectedInsuranceValue = 1000;
            const int productId = 1;
            const int productTypeId = 10;

            Product product = BuildProduct(productId, productTypeId, salesPrice: 1500);
            ProductType productType = BuildProductType(productTypeId, name: "Type", hasInsurance: true);

            var repository = new Mock<IProductRepository>();
            repository.Setup(p => p.GetProduct(productId)).ReturnsAsync(product);
            repository.Setup(p => p.GetProductType(productTypeId)).ReturnsAsync(productType);

            _productInsuranceService = new ProductInsuranceService(repository.Object);

            //act
            var result = await _productInsuranceService.CalculateProductInsurance(productId);

            //assert
            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result
            );
        }

        [Fact]
        public async Task CalculateProductInsurance_GivenSalesPriceMoreThan2000_ShouldAdd2000ToInsuranceCost()
        {
            //arrange
            const float expectedInsuranceValue = 2000;
            const int productId = 1;
            const int productTypeId = 10;

            Product product = BuildProduct(productId, productTypeId, salesPrice: 2500);
            ProductType productType = BuildProductType(productTypeId, name: "Type", hasInsurance: true);

            var repository = new Mock<IProductRepository>();
            repository.Setup(p => p.GetProduct(productId)).ReturnsAsync(product);
            repository.Setup(p => p.GetProductType(productTypeId)).ReturnsAsync(productType);

            _productInsuranceService = new ProductInsuranceService(repository.Object);

            //act
            var result = await _productInsuranceService.CalculateProductInsurance(productId);

            //assert
            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result
            );
        }

        [Theory]
        [InlineData("Laptops")]
        [InlineData("Smartphones")]
        public async Task CalculateProductInsurance_GivenProductTypeIsLaptopOrSmartphone_ShouldAdd500ToInsuranceCost(string productTypeName)
        {
            //arrange
            const float expectedInsuranceValue = 2500;
            const int productId = 1;
            const int productTypeId = 10;

            Product product = BuildProduct(productId, productTypeId, salesPrice: 2500);
            ProductType productType = BuildProductType(productTypeId, name: productTypeName, hasInsurance: true);

            var repository = new Mock<IProductRepository>();
            repository.Setup(p => p.GetProduct(productId)).ReturnsAsync(product);
            repository.Setup(p => p.GetProductType(productTypeId)).ReturnsAsync(productType);

            _productInsuranceService = new ProductInsuranceService(repository.Object);

            //act
            var result = await _productInsuranceService.CalculateProductInsurance(productId);

            //assert
            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result
            );
        }

        private static Product BuildProduct(int productId, int productTypeId, float salesPrice)
        {
            return new Product(productId,
                name: "Product Name",
                salesPrice,
                productTypeId);
        }

        private static ProductType BuildProductType(int productTypeId, string name, bool hasInsurance)
        {
            return new ProductType(productTypeId,
                name,
                hasInsurance);
        }
    }
}