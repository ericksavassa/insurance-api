using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace Insurance.Api.Controllers
{
    public class HomeController : Controller
    {
        [HttpPost]
        [Route("api/insurance/product")]
        public InsuranceDto CalculateInsurance([FromBody] InsuranceDto toInsure)
        {
            int productId = toInsure.ProductId;
            BusinessRules.GetProductType(ProductApi, productId, ref toInsure);
            BusinessRules.GetSalesPrice(ProductApi, productId, ref toInsure);

            toInsure.InsuranceValue = 0;
            if (toInsure.ProductTypeHasInsurance)
            {
                if (toInsure.SalesPrice < 500)
                    toInsure.InsuranceValue += 500;
                if (toInsure.SalesPrice > 500 && toInsure.SalesPrice < 2000)
                    toInsure.InsuranceValue += 1000;
                if (toInsure.SalesPrice >= 2000)
                    toInsure.InsuranceValue += 2000;
                if (toInsure.ProductTypeName == "Laptops" || toInsure.ProductTypeName == "Smartphones")
                    toInsure.InsuranceValue += 500;
            }

            return toInsure;
        }

        public class InsuranceDto
        {
            public int ProductId { get; set; }
            public float InsuranceValue { get; set; }
            [JsonIgnore]
            public string ProductTypeName { get; set; }
            [JsonIgnore]
            public bool ProductTypeHasInsurance { get; set; }
            [JsonIgnore]
            public float SalesPrice { get; set; }
        }

        private const string ProductApi = "http://localhost:5002";
    }
}