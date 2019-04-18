using System.Collections.Generic;

namespace StoreApp.Web.UI.Models
{
    /// <summary>
    /// Passed to ProductList view as Model
    /// </summary>
    public class ListViewModel<TDataType>
    {
        public IEnumerable<TDataType> Objects { get; set; }

        /// <summary>
        /// Information about paging
        /// </summary>
        public PagingInfo PagingInfo { get; set; }
    }
}