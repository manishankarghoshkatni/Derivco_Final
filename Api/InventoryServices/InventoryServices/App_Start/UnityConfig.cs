using System.Web.Http;
using Unity;
using Unity.WebApi;
using InventoryServices.Repository.Interfaces;
using InventoryServices.Repository.Classes;

namespace InventoryServices
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<ICategoryRepository, CategoryRepository>();
            container.RegisterType<IUnitRepository, UnitRepository>();
            container.RegisterType<IProductRepository, ProductRepository>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}