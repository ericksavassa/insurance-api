namespace Insurance.Domain.Entities
{
    /// <summary>
    /// Surcharge class.
    /// </summary>
    public class Surcharge
    {
        /// <summary>
        /// Contructor.
        /// </summary>
        /// <param name="ProductTypeId">Product type Id.</param>
        /// <param name="SurchargeRate">Surcharge Rate.</param>
        public Surcharge(int productTypeId, float surchargeRate)
        {
            this.ProductTypeId = productTypeId;
            this.SurchargeRate = surchargeRate;
        }

        public string Id { get; private set; }
        public int ProductTypeId { get; private set; }
        public float SurchargeRate { get; private set; }

        public void SetId(string id)
        {
            this.Id = id;
        }
    }
}