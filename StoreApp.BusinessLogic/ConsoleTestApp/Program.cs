using StoreApp.BusinessLogic.Abstract;
using StoreApp.BusinessLogic.Common.EF;
using StoreApp.BusinessLogic.Event;
using StoreApp.BusinessLogic.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new EFCommonDataContext())
            {
                IRepository<Category> categoryRepo = db;
                IRepository<Discount> discountRepo = db;

                Category testCategory = new Category
                {
                    DefaultName = "TestCategory2",
                    NameMessageID = 2
                };

                categoryRepo.Add(testCategory);
                categoryRepo.CommitChanges();

                foreach (var c in categoryRepo.ReadAll())
                {
                    Console.WriteLine(c.CategoryID);
                }

                Console.ReadKey(true);
            }
        }
    }
}
