using Ninject;
using StoreApp.Abstract.Interfaces;
using StoreApp.EventData;
using StoreApp.EventData.EF;
using StoreApp.LanguageData;
using StoreApp.LanguageData.Abstract;
using StoreApp.LanguageData.EF;
using StoreApp.LanguageData.LanguageHelpers;
using StoreApp.ProductData;
using StoreApp.ProductData.EF;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoreApp.Web.UI.Infrastructure
{
    public class CustomNinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public CustomNinjectDependencyResolver(IKernel kernel)
        {
            this.kernel = kernel;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        protected virtual void AddBindings()
        {
            //Gets connection string from web config
            string efConnectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;

            //One connection string is used for all contexts
            
            kernel.Bind<IRepository<Product>>().To<ProductDataContext>()
                .WithConstructorArgument("connectionString", efConnectionString);

            kernel.Bind<IRepository<Category>>().To<ProductDataContext>()
                .WithConstructorArgument("connectionString", efConnectionString);

            kernel.Bind<IRepository<Discount>>().To<EFDiscountDataContext>()
                .WithConstructorArgument("connectionString", efConnectionString);

            kernel.Bind<ILanguageRepository<MessageDetail>>().To<EFLanguageDataContext>()
                .WithConstructorArgument("connectionString", efConnectionString);

            kernel.Bind<ILanguageHelper>().To<StandartLanguageHelper>();
        }
    }
}