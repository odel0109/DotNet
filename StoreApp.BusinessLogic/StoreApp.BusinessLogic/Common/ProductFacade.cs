using StoreApp.Abstract.Extensions;
using StoreApp.Abstract.Interfaces;
using StoreApp.EventData;
using StoreApp.EventData.Enumerations;
using StoreApp.LanguageData;
using StoreApp.ProductData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace StoreApp.BusinessLogic.Common
{
    public class ProductFacade
    {
        private IRepository<Product> productRepository;
        private IRepository<Discount> discountRepository;
        private IRepository<Category> categoryRepository;

        private ILanguageHelper localizationHelper;

        public ProductFacade(IRepository<Product> productRepository, 
            IRepository<Discount> discountRepository, 
            IRepository<Category> categoryRepository,
            ILanguageHelper localizationHelper)
        {
            this.productRepository = productRepository;
            this.discountRepository = discountRepository;
            this.localizationHelper = localizationHelper;
            this.categoryRepository = categoryRepository;
        }

        #region implementation

        /// <summary>
        /// Returns actual discounts for the given date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public virtual List<Discount> GetActualDiscounts(DateTime date)
        {
            QueryParameterSet<Discount> parameterSet = new QueryParameterSet<Discount>
            {
                Criteria = disc => disc.Event.EndTime > date && disc.Event.StartTime < date
            };

            return discountRepository.Read(parameterSet)
                .ToList();
        }

        public virtual List<ProductExtended<Product, Discount>> ReadProductInformation(DateTime date, short userLanguage,
            QueryParameterSet<Product> parameterSet = null)
        {
            var products = productRepository.Read(parameterSet);

            var extendedProducts = ActualizePrice(date, products);

            //TODO: Fill descriptions and names
            FillProductsLocalizedInfo(extendedProducts);

            return extendedProducts;
        }

        /// <summary>
        /// returns information about 1 product
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public virtual ProductExtended<Product, Discount> GetProductById(int productID, DateTime date, short userLanguage)
        {
            var product = this.productRepository.Find(productID);

            if (product == null)
                return null;

            var productExtended = ActualizePrice(date, new[] { product });

            FillProductsLocalizedInfo(productExtended);

            return productExtended[0];
        }

        /// <summary>
        /// Searches discount for the given product and calculates price markdown.
        /// </summary>
        /// <param name="product"></param>
        /// <param name="discounts"></param>
        /// <returns></returns>
        public decimal? ProductSellingPrice(Product product, IEnumerable<Discount> discounts, out Discount appliedDiscount)
        {
            //In the current implementation 
            //If there is 2 discounts for one product/category
            // will be taken later discount

            appliedDiscount = null;

            //trying to find discount for product (it has higher priority than discount for category)
            var prodDiscount = FindDiscount(discounts, DiscountTargetTypes.Product, product.ProductID);

            if (prodDiscount != null)
            {
                appliedDiscount = prodDiscount;
                return CalculateDiscountPrice(product.Price, prodDiscount);
            }

            //trying to find discount for category
            var categoryDiscount = FindDiscount(discounts, DiscountTargetTypes.Category, product.Category.CategoryID);

            if(categoryDiscount != null)
            {
                appliedDiscount = categoryDiscount;
                return CalculateDiscountPrice(product.Price, categoryDiscount);
            }

            return null;
        }

        public virtual decimal CalculateDiscountPrice(decimal currentPrice, Discount discount)
        {
            switch (discount.DiscountType)
            {
                case DiscountTypes.AbsoluteValue:
                    return currentPrice - discount.Value;

                case DiscountTypes.Percent:
                    return currentPrice -(currentPrice / 100) * discount.Value;

                default:
                    return currentPrice;
            }
        }

        protected virtual List<ProductExtended<Product, Discount>> CreateProductExtensions(IEnumerable<Product> products)
        {
            var size = products != null ? products.Count() : 0;

            List<ProductExtended<Product, Discount>> result = new List<ProductExtended<Product, Discount>>(size);

            foreach (var product in products)
                result.Add(new ProductExtended<Product, Discount>(product));

            return result;
        }

        protected Discount FindDiscount(IEnumerable<Discount> discounts, 
            DiscountTargetTypes targetType, int targetIdentifier)
        {
            return discounts.
                Where(d => d.TargetType == targetType && d.Target == targetIdentifier)
                .OrderBy(d => d.Event.StartTime)
                .FirstOrDefault();
        }

        protected virtual void FillProductsLocalizedInfo(IEnumerable<ProductExtended<Product, Discount>> productExts)
        {
            //TODO: fill array from database
            var supportedLanguages = new[] { 1033, 1035 };

            foreach(var productExt in productExts)
                foreach(var langCD in supportedLanguages)
                {
                    var currentLanguageProductName = localizationHelper.GetText
                        (productExt.Product.NameMessageID,
                        (short)langCD,
                        productExt.Product.DefaultName);

                    var currentLanguageProductDesc = localizationHelper.GetText
                        (
                        productExt.Product.DescriptionMessageID,
                        (short)langCD,
                        "Default description");

                    productExt.LocalizedDescriptions.Add((short)langCD, currentLanguageProductDesc);
                    productExt.LocalizedNames.Add((short)langCD, currentLanguageProductName);
                }
        }

        /// <summary>
        /// Returns distict categories stored at product
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Category> GetAllCategories()
        {
            return categoryRepository.ReadAll();
        }

        /// <summary>
        /// Returns all categories with localized name
        /// </summary>
        /// <param name="userLanguage"></param>
        /// <returns></returns>
        public Dictionary<Category, string> GetAllCategoriesWithDescription(short userLanguage)
        {
            var categories = GetAllCategories();

            Dictionary<Category, string> result = new Dictionary<Category, string>(categories.Count());

            foreach (var category in categories)
            {
                result.Add(category, localizationHelper.GetText(category.NameMessageID, userLanguage, category.DefaultName));
            }

            return result;
        }

        public int CalculateCountOfProducts(string category = "All")
        {
            QueryParameterSet<Product> queryParameter = new QueryParameterSet<Product>
            {
                Criteria = x => category == "All" || x.Category.DefaultName == category
            };

            return productRepository.Read(queryParameter).Count();
        }

        protected virtual List<ProductExtended<Product, Discount>> ActualizePrice(DateTime date, IEnumerable<Product> products)
        {
            var actualDiscounts = GetActualDiscounts(date);

            var extendedProducts = CreateProductExtensions(products);

            extendedProducts.ForEach(x =>
            {
                Discount appliedDiscount;
                x.ActualPrice = ProductSellingPrice(x.Product, actualDiscounts, out appliedDiscount);
                x.AppliedDiscount = appliedDiscount;
            });

            return extendedProducts;
        }
        #endregion
    }
}
