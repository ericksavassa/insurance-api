using Insurance.Application.Services;
using Insurance.Application.Services.Interfaces;
using Insurance.Domain.Entities;
using Insurance.Domain.Interfaces.Repositories;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Insurance.Tests.Application
{
    public class ProductInsuranceServiceTest
    {
        private IProductInsuranceService _productInsuranceService;

        [Fact]
        public async Task CalculateProductInsurance_GivenSalesPriceLessThan500_ShouldAdd500ToInsuranceCost()
        {
            //arrange
            const float expectedInsuranceValue = 500;
            const int productId = 1;
            const int productTypeId = 10;
            Product product = BuildProduct(productId, productTypeId, salesPrice: 400);
            ProductType productType = BuildProductType(productTypeId, name: "Type", hasInsurance: true);

            var surchargeRepository = new Mock<ISurchargeRepository>();
            var productRepository = new Mock<IProductRepository>();
            productRepository.Setup(p => p.GetProduct(productId)).ReturnsAsync(product);
            productRepository.Setup(p => p.GetProductType(productTypeId)).ReturnsAsync(productType);
                        
            _productInsuranceService = new ProductInsuranceService(productRepository.Object, surchargeRepository.Object);

            //act
            var result = await _productInsuranceService.CalculateProductInsurance(productId);

            //assert
            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result
            );
        }

        [Fact]
        public async Task CalculateProductInsurance_GivenSalesPriceLessThan500AndHas20SurchargeRate_ShouldAdd520ToInsuranceCost()
        {
            //arrange
            const float expectedInsuranceValue = 520;
            const int productId = 1;
            const int productTypeId = 10;
            Product product = BuildProduct(productId, productTypeId, salesPrice: 400);
            ProductType productType = BuildProductType(productTypeId, name: "Type", hasInsurance: true);
            Surcharge surcharge = BuildSurcharge(productTypeId, surchargeRate: 20);

            var surchargeRepository = new Mock<ISurchargeRepository>();
            var productRepository = new Mock<IProductRepository>();
            productRepository.Setup(p => p.GetProduct(productId)).ReturnsAsync(product);
            productRepository.Setup(p => p.GetProductType(productTypeId)).ReturnsAsync(productType);
            surchargeRepository.Setup(p => p.GetByProductTypeId(productTypeId)).ReturnsAsync(surcharge);

            _productInsuranceService = new ProductInsuranceService(productRepository.Object, surchargeRepository.Object);

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

            var surchargeRepository = new Mock<ISurchargeRepository>();
            var productRepository = new Mock<IProductRepository>();
            productRepository.Setup(p => p.GetProduct(productId)).ReturnsAsync(product);
            productRepository.Setup(p => p.GetProductType(productTypeId)).ReturnsAsync(productType);

            _productInsuranceService = new ProductInsuranceService(productRepository.Object, surchargeRepository.Object);

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

            var surchargeRepository = new Mock<ISurchargeRepository>();
            var productRepository = new Mock<IProductRepository>();
            productRepository.Setup(p => p.GetProduct(productId)).ReturnsAsync(product);
            productRepository.Setup(p => p.GetProductType(productTypeId)).ReturnsAsync(productType);

            _productInsuranceService = new ProductInsuranceService(productRepository.Object, surchargeRepository.Object);

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

            var surchargeRepository = new Mock<ISurchargeRepository>();
            var productRepository = new Mock<IProductRepository>();
            productRepository.Setup(p => p.GetProduct(productId)).ReturnsAsync(product);
            productRepository.Setup(p => p.GetProductType(productTypeId)).ReturnsAsync(productType);

            _productInsuranceService = new ProductInsuranceService(productRepository.Object, surchargeRepository.Object);

            //act
            var result = await _productInsuranceService.CalculateProductInsurance(productId);

            //assert
            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result
            );
        }

        [Fact]
        public async Task CalculateInsuranceForProductList_Given2ProductsWithSalesPriceLessThan500_ShouldAdd1000ToInsuranceCost()
        {
            //arrange
            const float expectedInsuranceValue = 1000;
            const int productId1 = 1;
            const int productId2 = 2;
            const int productTypeId = 10;
            var productList = new List<int>() { productId1, productId2 };

            Product product1 = BuildProduct(productId1, productTypeId, salesPrice: 300);
            Product product2 = BuildProduct(productId2, productTypeId, salesPrice: 300);
            ProductType productType = BuildProductType(productTypeId, name: "Type", hasInsurance: true);

            var surchargeRepository = new Mock<ISurchargeRepository>();
            var productRepository = new Mock<IProductRepository>();
            productRepository.Setup(p => p.GetProduct(productId1)).ReturnsAsync(product1);
            productRepository.Setup(p => p.GetProduct(productId2)).ReturnsAsync(product2);
            productRepository.Setup(p => p.GetProductType(productTypeId)).ReturnsAsync(productType);

            _productInsuranceService = new ProductInsuranceService(productRepository.Object, surchargeRepository.Object);

            //act
            var result = await _productInsuranceService.CalculateInsuranceForProductList(productList);

            //assert
            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result
            );
        }

        [Fact]
        public async Task CalculateInsuranceForProductList_Given2ProductsThatAreDigitalCameras_ShouldAdd1500ToInsuranceCost()
        {
            //arrange
            const float expectedInsuranceValue = 1500;
            const int productId1 = 1;
            const int productId2 = 2;
            const int productTypeId = 10;
            var productList = new List<int>() { productId1, productId2 };

            Product product1 = BuildProduct(productId1, productTypeId, salesPrice: 300);
            Product product2 = BuildProduct(productId2, productTypeId, salesPrice: 300);
            ProductType productType = BuildProductType(productTypeId, name: "Digital cameras", hasInsurance: true);
            
            var surchargeRepository = new Mock<ISurchargeRepository>();
            var productRepository = new Mock<IProductRepository>();
            productRepository.Setup(p => p.GetProduct(productId1)).ReturnsAsync(product1);
            productRepository.Setup(p => p.GetProduct(productId2)).ReturnsAsync(product2);
            productRepository.Setup(p => p.GetProductType(productTypeId)).ReturnsAsync(productType);

            _productInsuranceService = new ProductInsuranceService(productRepository.Object, surchargeRepository.Object);

            //act
            var result = await _productInsuranceService.CalculateInsuranceForProductList(productList);

            //assert
            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result
            );
        }

        [Fact]
        public async Task CalculateProductInsurance_GivenProductTypeThatCanNotBeInsured_ShouldInsuranceCostBe0()
        {
            //arrange
            const float expectedInsuranceValue = 0;
            const int productId = 1;
            const int productTypeId = 10;
            Product product = BuildProduct(productId, productTypeId, salesPrice: 400);
            ProductType productType = BuildProductType(productTypeId, name: "Type", hasInsurance: false);

            var surchargeRepository = new Mock<ISurchargeRepository>();
            var productRepository = new Mock<IProductRepository>();
            productRepository.Setup(p => p.GetProduct(productId)).ReturnsAsync(product);
            productRepository.Setup(p => p.GetProductType(productTypeId)).ReturnsAsync(productType);

            _productInsuranceService = new ProductInsuranceService(productRepository.Object, surchargeRepository.Object);

            //act
            var result = await _productInsuranceService.CalculateProductInsurance(productId);

            //assert
            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result
            );
        }

        private Product BuildProduct(int productId, int productTypeId, float salesPrice)
        {
            return new Product(productId,
                name: "Product Name",
                salesPrice,
                productTypeId);
        }

        private ProductType BuildProductType(int productTypeId, string name, bool hasInsurance)
        {
            return new ProductType(productTypeId,
                name,
                hasInsurance);
        }

        private Surcharge BuildSurcharge(int productTypeId, float surchargeRate)
        {
            return new Surcharge(productTypeId, surchargeRate);
        }
    }
}