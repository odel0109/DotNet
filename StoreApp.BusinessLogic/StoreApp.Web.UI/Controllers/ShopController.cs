using StoreApp.Abstract.Extensions;
using StoreApp.BusinessLogic.Common;
using StoreApp.EventData;
using StoreApp.ProductData;
using StoreApp.Web.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoreApp.Web.UI.Controllers
{
    public class ShopController : Controller
    {
        private ProductFacade productFacade;

        /// <summary>
        /// TODO: Change to dynamic definition
        /// </summary>
        private short userLanguageCode = 1033;

        /// <summary>
        /// Change to dynamic definition
        /// </summary>
        public int itemsPerPage = 3;

        public ShopController(ProductFacade productFacade)
        {
            this.productFacade = productFacade;
        }

        public ViewResult ProductList(int page, string selectedCategory = "All")
        {
            var products = GetProducts(page, selectedCategory, userLanguageCode, itemsPerPage);

            //preparing paging information
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = page,
                ItemsPerPage = itemsPerPage,
                TotalItems = productFacade.CalculateCountOfProducts(selectedCategory)
            };

            //preparing view model
            ProductListViewModel shopViewModel = new ProductListViewModel
            {
                Objects = products,
                CurrentCategory = selectedCategory,
                PagingInfo = pagingInfo
            };

            return View(shopViewModel);
        }

        public ViewResult ProductDetails(int id)
        {
             var product = productFacade.GetProductById(id, DateTime.Now, userLanguageCode);

            return View(product);
        }

        protected virtual List<ProductExtended<Product,Discount>> GetProducts(int page, string selectedCategory, 
            short userLanguage, int itemsPerPageParam)
        {
            //reading information related to product
            QueryParameterSet<Product> queryParameterSet = new QueryParameterSet<Product>
            {
                Criteria = p => selectedCategory == "All" || p.Category.DefaultName == selectedCategory,
                OrderByPropertyName = "ProductID",
                SkipCount = (page - 1) * itemsPerPageParam,
                TakeCount = itemsPerPageParam
            };

            return productFacade.ReadProductInformation(DateTime.Now, userLanguage, queryParameterSet);
        }

    }
}