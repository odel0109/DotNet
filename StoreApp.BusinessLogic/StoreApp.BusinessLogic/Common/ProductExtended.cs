using StoreApp.EventData;
using StoreApp.ProductData;
using System.Collections.Generic;

namespace StoreApp.BusinessLogic.Common
{
    /// <summary>
    /// Extension for product entity
    /// </summary>
    /// <typeparam name="TProdType"></typeparam>
    public class ProductExtended<TProdType, TDiscountType>
        where TProdType : Product, new()
        where TDiscountType: Discount
    {
        public ProductExtended(TProdType product)
        {
            this.Product = product;
            LocalizedNames = new Dictionary<short, string>();
            LocalizedDescriptions = new Dictionary<short, string>();
        }

        /// <summary>
        /// Product object
        /// </summary>
        public TProdType Product { get; }

        /// <summary>
        /// Price after applying discount
        /// </summary>
        public decimal? ActualPrice { get; set; }

        /// <summary>
        /// Discount which caused price markdown
        /// </summary>
        public TDiscountType AppliedDiscount { get; set; }

        /// <summary>
        /// Name depending on user language
        /// </summary>
        public Dictionary<short, string> LocalizedNames { get; set; }

        /// <summary>
        /// Description depending user language
        /// </summary>
        public Dictionary<short, string> LocalizedDescriptions { get; set; }
    }
}
