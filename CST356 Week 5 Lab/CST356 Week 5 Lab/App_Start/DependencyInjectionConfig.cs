using CST356_Week_5_Lab.Data;
using CST356_Week_5_Lab.Repositories;
using CST356_Week_5_Lab.Services;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace CST356_Week_5_Lab.App_Start
{
    public static class DependencyInjectionConfig
    {
        public static void Register()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            
            container.Register<IAppRepository, AppRepository>(Lifestyle.Scoped);
            container.Register<MyDbContext, MyDbContext>(Lifestyle.Scoped);
            container.Register<IPetService, PetService>(Lifestyle.Scoped);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}