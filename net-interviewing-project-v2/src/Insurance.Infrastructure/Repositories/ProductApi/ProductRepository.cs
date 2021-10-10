using Insurance.Domain.Exceptions;
using Insurance.Domain.Interfaces.Repositories;
using Insurance.Infrastructure.ProductApi.Dtos;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Entities = Insurance.Domain.Entities;

namespace Insurance.Infrastructure.Repositories.ProductApi
{
    public class ProductRepository : IProductRepository
    {
        private readonly string PRODUCT_API_CLIENT_NAME = "ProductApiClient";
        private readonly string PRODUCT_TYPE_BY_ID_RESOURCE = "/product_types/{0:G}";
        private readonly string PRODUCT_BY_ID_RESOURCE = "/products/{0:G}";
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<ProductRepository> _logger;

        public ProductRepository(ILogger<ProductRepository> logger, IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _logger = logger;
        }

        public async Task<Entities.Product> GetProduct(int productId)
        {
            try
            {
                var resource = string.Format(PRODUCT_BY_ID_RESOURCE, productId);
                var httpClient = _clientFactory.CreateClient(PRODUCT_API_CLIENT_NAME);

                using var response = await httpClient.GetAsync(resource, HttpCompletionOption.ResponseHeadersRead);
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                var product = JsonConvert.DeserializeObject<Product>(json);

                return new Entities.Product(product.Id,
                    product.Name,
                    product.SalesPrice,
                    product.ProductTypeId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting Product from ProductApi!");
                throw new ProductNotFoundException(productId);
            }
        }

        public async Task<Entities.ProductType> GetProductType(int productTypeId)
        {
            try
            {
                var resource = string.Format(PRODUCT_TYPE_BY_ID_RESOURCE, productTypeId);
                var httpClient = _clientFactory.CreateClient(PRODUCT_API_CLIENT_NAME);

                using var response = await httpClient.GetAsync(resource, HttpCompletionOption.ResponseHeadersRead);
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                var productType = JsonConvert.DeserializeObject<ProductType>(json);

                return new Entities.ProductType(productType.Id,
                    productType.Name,
                    productType.CanBeInsured);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting ProductType from ProductApi!");
                throw new ProductTypeNotFoundException(productTypeId);
            }
        }
    }
}