using StoreApp.BusinessLogic.Common;
using StoreApp.EventData;
using StoreApp.ProductData;

namespace StoreApp.Web.UI.Models
{
    public class ProductListViewModel : ListViewModel<ProductExtended<Product, Discount>>
    {
        public string CurrentCategory { get; set; }
    }
}