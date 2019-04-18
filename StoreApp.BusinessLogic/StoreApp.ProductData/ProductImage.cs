using StoreApp.Abstract.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreApp.ProductData
{
    /// <summary>
    /// Images of product
    /// </summary>
    public class ProductImage : IEntity
    {
        /// <summary>
        /// Product to which image is attached
        /// </summary>
        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }

        /// <summary>
        /// ProductID of image
        /// </summary>
        public int ProductID { get; set; }

        /// <summary>
        /// Sequence number. Image with sequence number = 0 can be used as main image of product
        /// </summary>
        public short SequenceNumber { get; set; }

        /// <summary>
        /// Image binary data
        /// </summary>
        public byte[] ImageData { get; set; }

        /// <summary>
        /// Mime type of image
        /// </summary>
        public string ImageMimeType { get; set; }
        public object[] PrimaryKey => new object[] { Product, SequenceNumber };
    }
}
