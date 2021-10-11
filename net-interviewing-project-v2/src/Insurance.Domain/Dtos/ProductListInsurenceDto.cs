using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Insurance.Domain.Dtos
{
    public class ProductListInsurenceDto
    {
        [Required]
        public List<int> ProductIds { get; set; }
        public float InsuranceValue { get; set; }
    }
}