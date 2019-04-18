using StoreApp.BusinessLogic.Common;
using System.Web.Mvc;

namespace StoreApp.Web.UI.Controllers
{
    /// <summary>
    /// Controller for menu
    /// </summary>
    public class NavigationController : Controller
    {
        private ProductFacade productFacade;
        private short userLanguageCode = 1033;

        public NavigationController(ProductFacade productFacade)
        {
            this.productFacade = productFacade;
        }

        public PartialViewResult Menu(string selectedCategory)
        {
            ViewBag.SelectedCategory = selectedCategory;

            var categories = this.productFacade.GetAllCategoriesWithDescription(userLanguageCode);

            return PartialView(categories);
        }
    }
}