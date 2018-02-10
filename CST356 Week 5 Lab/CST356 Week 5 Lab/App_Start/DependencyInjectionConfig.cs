using CST356_Week_5_Lab.Data;
using CST356_Week_5_Lab.Repositories;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CST356_Week_5_Lab.App_Start
{
    public static class DependencyInjectionConfig
    {
        public static void Register()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            // Register your types, for instance:
            container.Register<IAppRepository, AppRepository>(Lifestyle.Scoped);
            container.Register<MyDbContext, MyDbContext>(Lifestyle.Scoped);
        }
}