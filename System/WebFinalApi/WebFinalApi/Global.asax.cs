using AFinalDAL;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Compilation;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebFinalApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            RegisterAll();
        }

        public void RegisterAll()
        {
            //创建autofac管理注册类的容器实例
            var builder = new ContainerBuilder();

            HttpConfiguration config = GlobalConfiguration.Configuration;
            // 通常仅通过接口暴露服务
            builder.RegisterType<CommonDB>();
            //注册所有实现了  Service接口的类型
            var dataAccess = Assembly.GetExecutingAssembly();
            var needClass = dataAccess.ExportedTypes.Where(item => (item.Name.EndsWith("Service")) && !item.IsAbstract).ToArray();
            builder.RegisterTypes(needClass).AsSelf().AsImplementedInterfaces()
                      .PropertiesAutowired().InstancePerLifetimeScope();

            //注册Api类型
            builder.RegisterApiControllers(dataAccess).PropertiesAutowired();
            IContainer container = builder.Build();
            //注册api容器需要使用HttpConfiguration对象
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            //注册解析
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

    }
}
