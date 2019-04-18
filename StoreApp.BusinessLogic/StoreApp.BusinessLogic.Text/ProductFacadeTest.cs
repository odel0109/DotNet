using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StoreApp.Abstract.Extensions;
using StoreApp.Abstract.Interfaces;
using StoreApp.BusinessLogic.Common;
using StoreApp.EventData;
using StoreApp.EventData.Enumerations;
using StoreApp.LanguageData;
using StoreApp.ProductData;

namespace StoreApp.BusinessLogic.Text
{
    [TestClass]
    public class ProductFacadeTest
    {
        /// <summary>
        /// Test that absolute and percent discount are calculating correct values
        /// </summary>
        [TestMethod]
        public void CalculateDiscountPriceTest()
        {
            var testProducts = PrepareProductList();
            var testDiscounts = PrepareDiscountList();

            var facade = CreateTestFacade(testDiscounts, testProducts);

            Discount testDiscount = new Discount
            {
                DiscountType = DiscountTypes.AbsoluteValue,
                MinQuantity = 5,
                Target = 1,
                TargetType = DiscountTargetTypes.Product,
                Value = 20
            };

            var valueAFterAbsoluteDiscount = facade.CalculateDiscountPrice(50, testDiscount);

            testDiscount.DiscountType = DiscountTypes.Percent;

            var valueAfterPersentDiscount = facade.CalculateDiscountPrice(50, testDiscount);

            Assert.AreEqual(valueAFterAbsoluteDiscount, 30m);
            Assert.AreEqual(valueAfterPersentDiscount, 40m);
        }

        /// <summary>
        /// Check that between Category iscount and product discount system selects product discount
        /// </summary>
        [TestMethod]
        public void ProductSellingPriceTest()
        {
            var testProducts = PrepareProductList();
            var testDiscounts = PrepareDiscountList();

            var facade = CreateTestFacade(testDiscounts, testProducts);

            Discount appliedDiscount = null;
            var newPrice = facade.ProductSellingPrice(testProducts.ElementAt(0), testDiscounts, out appliedDiscount);

            Assert.IsNotNull(appliedDiscount);
            Assert.AreEqual(appliedDiscount.TargetType, DiscountTargetTypes.Product);
            Assert.AreEqual(newPrice, 90m);
        }

        [TestMethod]
        public void CorrectProductLenghtTest()
        {
            var testProducts = PrepareProductList();
            var testDiscounts = PrepareDiscountList();

            var facade = CreateTestFacade(testDiscounts, testProducts);

            var result = facade.ReadProductInformation(It.IsAny<DateTime>(), 9);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count, testProducts.Count());
        }

        private ProductFacade CreateTestFacade(
            IQueryable<Discount> testDiscounts, IQueryable<Product> testProducts)
        {
            Mock<ILanguageHelper> mockLanguageHelper = new Mock<ILanguageHelper>();
            mockLanguageHelper.Setup(x => x.GetText(It.IsAny<int>(), It.IsAny<short>(), It.IsAny<string>())).Returns("default");

            Mock<IRepository<Product>> mockProductRepo = new Mock<IRepository<Product>>();
            mockProductRepo.Setup(p => p.Read(It.IsAny<QueryParameterSet<Product>>())).Returns(testProducts);

            Mock<IRepository<Discount>> mockDiscountRepo = new Mock<IRepository<Discount>>();
            mockDiscountRepo.Setup(d => d.Read(It.IsAny<QueryParameterSet<Discount>>())).Returns(testDiscounts);

            return new ProductFacade(mockProductRepo.Object,
             mockDiscountRepo.Object, null, mockLanguageHelper.Object);
        }

        private IQueryable<Product> PrepareProductList()
        {
            Category firstCategory = new Category
            {
                CategoryID = 1,
                DefaultName = "DefaultName1",
                NameMessageID = 111
            };

            Category secondCategory = new Category
            {
                CategoryID = 2,
                DefaultName = "DefaultName2",
                NameMessageID = 222
            };

            var list = new List<Product>();

            list.Add(new Product
            {
                ProductID = 1,
                Price = 100,
                Category = firstCategory,
                NameMessageID = 1,
                DefaultName = "",
                DescriptionMessageID = 11
            });

            list.Add(new Product
            {
                ProductID = 2,
                Price = 200,
                Category = secondCategory,
                NameMessageID = 2,
                DefaultName = "",
                DescriptionMessageID = 222
            });

            return list.AsQueryable();
        }

        private IQueryable<Discount> PrepareDiscountList()
        {
            Event defaultEvent = new Event
            {
                Name = "defaultEvent",
                StartTime = DateTime.Parse("01.01.2000"),
                EndTime = DateTime.Parse("01.01.2100")
            };

            Discount d1 = new Discount
            {
                Event = defaultEvent,
                DiscountType = DiscountTypes.AbsoluteValue,
                MinQuantity = 5,
                TargetType = DiscountTargetTypes.Product,
                Target = 1,
                Value = 10
            };

            Discount discount1Category = new Discount
            {
                Event = defaultEvent,
                DiscountType = DiscountTypes.AbsoluteValue,
                MinQuantity = 5,
                TargetType = DiscountTargetTypes.Category,
                Target = 1,
                Value = 10
            };

            Discount d2 = new Discount
            {
                Event = defaultEvent,
                DiscountType = DiscountTypes.Percent,
                MinQuantity = 5,
                TargetType = DiscountTargetTypes.Product,
                Target = 2,
                Value = 50
            };

            return new List<Discount> { d1, d2, discount1Category }.AsQueryable();
        }
    }
}
