using System.ComponentModel.DataAnnotations;

namespace Insurance.Domain.Dtos
{
    public class ProductTypeSurchargeDto
    {
        [Range(1, int.MaxValue)]
        public int ProductTypeId { get; set; }
        public float SurchargeRate { get; set; }
    }
}