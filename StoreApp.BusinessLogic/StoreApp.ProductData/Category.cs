using StoreApp.Abstract.Interfaces;
using System.Collections.Generic;

namespace StoreApp.ProductData
{
    public class Category : IEntity
    {
        /// <summary>
        /// Identifier of product
        /// </summary>
        public int CategoryID { get; set; }

        /// <summary>
        /// Default name of category
        /// </summary>
        public string DefaultName { get; set; }

        /// <summary>
        /// Message I of category`s name
        /// </summary>
        public int NameMessageID { get; set; }

        /// <summary>
        /// Products of category
        /// </summary>
        public virtual ICollection<Product> Products { get; set; }

        /// <summary>
        /// Primary Key of categoty
        /// </summary>
        public object[] PrimaryKey => new object[] { CategoryID };

        public Category()
        {
            Products = new List<Product>();
        }
    }
}
