using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Insurance.Api.Dtos
{
    public class ProductListInsurenceDto
    {
        [Required]
        public List<int> ProductIds { get; set; }
        public float InsuranceValue { get; set; }
    }
}