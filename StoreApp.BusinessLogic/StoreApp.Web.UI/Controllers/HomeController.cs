using StoreApp.BusinessLogic.Common;
using StoreApp.EventData;
using StoreApp.LanguageData;
using StoreApp.ProductData;
using StoreApp.ProductData.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoreApp.Web.UI.Controllers
{
    public class HomeController : Controller
    {
        private ProductFacade productFacade;
        private short userLanguageCode = 1033;

        public HomeController(ProductFacade productFacade)
        {
            this.productFacade = productFacade;
        }


        // GET: Home
        public ActionResult Index()
        {
            var products = this.productFacade.ReadProductInformation(DateTime.Now, userLanguageCode);

            return View(products);
        }
    }
}