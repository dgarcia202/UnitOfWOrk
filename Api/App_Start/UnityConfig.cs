using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;

namespace Api
{
    using Application;
    using Application.Infrastructure;

    using Entities;

    using Infrastructure;

    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers

            container.RegisterType<IOrderManagementService, OrderManagementService>();

            container.RegisterType<IRepository<Provider>, Repository<Provider>>();
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}