namespace Insurance.Domain.Entities
{
    /// <summary>
    /// Product Type class.
    /// </summary>
    public class ProductType
    {
        /// <summary>
        /// Contructor.
        /// </summary>
        /// <param name="id">Product type id.</param>
        public ProductType(int id)
        {
            this.Id = id;
        }

        /// <summary>
        /// Contructor.
        /// </summary>
        /// <param name="id">Product type id.</param>
        /// <param name="name">Product type name.</param>
        /// <param name="hasInsurance">Flag to check if product type calculate insurance.</param>
        public ProductType(int id, string name, bool hasInsurance)
        {
            this.Id = id;
            this.Name = name;
            this.HasInsurance = hasInsurance;
        }

        public int Id { get; set; }
        public string Name { get; private set; }
        public bool HasInsurance { get; private set; }
    }
}