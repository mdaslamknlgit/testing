using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using MyURLLoyalty.Business.Interface;
using MyURLLoyalty.Business.Register;
using MyURLLoyalty.Business.Users;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;

namespace MyUELLoyalty.API.App_Start
{
    public class IOCConfig
    {
        public static void RegisterManagers(HttpConfiguration config)
        {
            var container = new SimpleInjector.Container();

            //Register
            container.Register<IRegisterBusiness, RegisterBusiness>(Lifestyle.Transient);

            //User
            container.Register<IUsersBusiness, UsersBusiness>(Lifestyle.Transient);

            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }
    }
}