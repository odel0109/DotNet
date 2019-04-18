using StoreApp.Abstract.Interfaces;
using StoreApp.EventData.Enumerations;

namespace StoreApp.EventData
{
    /// <summary>
    /// Discount business entity
    /// </summary>
    public class Discount : IEntity
    {
        /// <summary>
        /// Identifier of discount
        /// </summary>
        public int DiscountID { get; set; }

        public int EventID { get; set; }

        /// <summary>
        /// Identifier of parent event.
        /// </summary>
        public virtual Event Event { get; set; }

        /// <summary>
        /// Type of disocunt (percent/absolute value ect.)
        /// </summary>
        public DiscountTypes DiscountType { get; set; }

        /// <summary>
        /// Value of discount
        /// </summary>
        public decimal Value { get; set; }

        /// <summary>
        /// Type of target of discount (per one product/per category ect.)
        /// </summary>
        public DiscountTargetTypes TargetType { get; set; }

        /// <summary>
        /// Identifier of target
        /// </summary>
        public int Target { get; set; }

        /// <summary>
        /// Quantity for step discounts. For example "Buy 3 products, get 10% descount"
        /// </summary>
        public int? MinQuantity { get; set; }

        public object[] PrimaryKey => new object[] { DiscountID };
    }
}
