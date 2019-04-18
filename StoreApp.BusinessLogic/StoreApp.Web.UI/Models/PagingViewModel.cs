using System;

namespace StoreApp.Web.UI.Models
{
    public class PagingViewModel
    {
        /// <summary>
        /// Information about paging
        /// </summary>
        public PagingInfo PagingInfo { get; set; }

        /// <summary>
        /// Delegate which generates url for links
        /// </summary>
        public Func<int, string> UrlGenerationRule { get; set; }
    }
}