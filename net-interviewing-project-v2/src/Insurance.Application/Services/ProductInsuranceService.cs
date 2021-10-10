using Insurance.Application.Services.Interfaces;
using Insurance.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Insurance.Application.Services
{
    public class ProductInsuranceService : IProductInsuranceService
    {
        private readonly IProductRepository _productRepository;

        public ProductInsuranceService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<float> CalculateProductInsurance(int productId)
        {
            var product = await _productRepository.GetProduct(productId);
            var productType = await _productRepository.GetProductType(product.ProductTypeId);

            if (!productType.HasInsurance)
                return 0;

            var insuranceValue = GetProductInsuranceBySalesPrice(product.SalesPrice);
            product.AddInsuranceValue(insuranceValue);

            insuranceValue = GetProductInsuranceByType(productType.Name);
            product.AddInsuranceValue(insuranceValue);

            return product.InsuranceValue;
        }

        public async Task<float> CalculateInsuranceToProductList(IList<int> productIdsList)
        {
            float toInsureValue = 0;
            foreach (var productId in productIdsList)
                toInsureValue += await CalculateProductInsurance(productId);            
            return toInsureValue;
        }

        private float GetProductInsuranceBySalesPrice(float salesPrice)
        {
            if (salesPrice < 500)
                return 500;
            else if (salesPrice >= 500 && salesPrice < 2000)
                return 1000;
            else if (salesPrice >= 2000)
                return 2000;

            return 0;
        }

        private float GetProductInsuranceByType(string productType)
        {
            if (productType == "Laptops" || productType == "Smartphones")
                return 500;

            return 0;
        }
    }
}