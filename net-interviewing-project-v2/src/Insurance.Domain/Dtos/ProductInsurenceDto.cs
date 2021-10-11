using System.ComponentModel.DataAnnotations;

namespace Insurance.Domain.Dtos
{
    public class ProductInsurenceDto
    {
        [Range(1, int.MaxValue)]
        public int ProductId { get; set; }
        public float InsuranceValue { get; set; }
    }
}