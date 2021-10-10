using Insurance.Application.Services.Interfaces;
using Insurance.Domain.Entities;
using Insurance.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;
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
            var product = await CalculateProductInsuranceByProductId(productId);
            return product.InsuranceValue;
        }
        public async Task<float> CalculateInsuranceForProductList(IList<int> productIdsList)
        {
            float toInsureValue = 0;
            var productList = new List<Product>();

            foreach (var productId in productIdsList)
            {
                var product = await CalculateProductInsuranceByProductId(productId);
                toInsureValue += product.InsuranceValue;
                productList.Add(product);
            }
            
            toInsureValue += GetInsuranceForProductListByProductType(productList);
            return toInsureValue;
        }

        private async Task<Product> CalculateProductInsuranceByProductId(int productId)
        {
            var product = await _productRepository.GetProduct(productId);
            var productType = await _productRepository.GetProductType(product.ProductTypeId);
            product.SetProductType(productType);

            if (!productType.HasInsurance)
                return product;

            var insuranceValue = GetProductInsuranceBySalesPrice(product.SalesPrice);
            product.AddInsuranceValue(insuranceValue);

            insuranceValue = GetProductInsuranceByType(product.ProductType.Name);
            product.AddInsuranceValue(insuranceValue);

            return product;
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

        private float GetInsuranceForProductListByProductType(List<Product> productsList)
        {
            if(productsList.Any(p => p.ProductType.HasInsurance && p.ProductType.Name == "Digital cameras"))
                return 500;

            return 0;
        }
    }
}