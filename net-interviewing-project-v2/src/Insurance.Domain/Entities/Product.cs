namespace Insurance.Domain.Entities
{
    /// <summary>
    /// Product class.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// The contructor.
        /// </summary>
        /// <param name="id">Product id.</param>
        /// <param name="name">Product name.</param>
        /// <param name="salesPrice">Product sales price.</param>
        public Product(int id, string name, float salesPrice, int producTypeId)
        {
            this.Id = id;
            this.Name = name;
            this.SalesPrice = salesPrice;
            this.InsuranceValue = 0;
            this.ProductTypeId = producTypeId;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public float InsuranceValue { get; private set; }
        public float SalesPrice { get; private set; }
        public int ProductTypeId { get; private set; }

        /// <summary>
        /// Add value to insurance of the product.
        /// </summary>
        /// <param name="value">Value to be added.</param>
        public void AddInsuranceValue(float value)
        {
            this.InsuranceValue += value;
        }
    }
}