using StoreApp.Abstract.Interfaces;
using System;
using System.Collections.Generic;

namespace StoreApp.EventData
{

    /// <summary>
    /// Event business entity. For example "Summer sale", "New Year event" ect.
    /// </summary>
    public class Event : IEntity
    {
        /// <summary>
        /// Identifier of event
        /// </summary>
        public int EventID { get; set; }

        /// <summary>
        /// Start date of event
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// End date of event
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Name of event
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Discounts of the event
        /// </summary>
        public virtual ICollection<Discount> Discounts { get; set; }

        /// <summary>
        /// Primary key of event
        /// </summary>
        public object[] PrimaryKey => new object[] { EventID };
    }
}
