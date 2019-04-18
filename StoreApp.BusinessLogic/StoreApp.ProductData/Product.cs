using StoreApp.Abstract.Interfaces;
using System.Collections.Generic;

namespace StoreApp.ProductData
{
    /// <summary>
    /// Product business entity
    /// </summary>
    public class Product : IEntity
    {
        /// <summary>
        /// Identifier of product
        /// </summary>
        public int ProductID { get; set; }

        /// <summary>
        /// Name of product which will be shown if there is no localization of name
        /// </summary>
        public string DefaultName { get; set; }

        /// <summary>
        /// Message ID of product`s name
        /// </summary>
        public int NameMessageID { get; set; }

        /// <summary>
        /// Message ID which contains description of product
        /// </summary>
        public int DescriptionMessageID { get; set; }

        /// <summary>
        /// Price of product
        /// </summary>
        public decimal Price { get; set; }

        public int CategoryID { get; set; }

        /// <summary>
        /// Category of product
        /// </summary>
        public virtual Category Category { get; set; }

        /// <summary>
        /// Shows that product is active for sale
        /// </summary>
        public bool ActiveFlag { get; set; }

        /// <summary>
        /// Images of product
        /// </summary>
        public virtual ICollection<ProductImage> ProductImages { get; set; }

        /// <summary>
        /// Primary key of prouct
        /// </summary>
        public object[] PrimaryKey => new object[] { ProductID };

        public Product()
        {
            ProductImages = new List<ProductImage>();
        }
    }
}
