using AFinalDAL;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Senparc;
using Senparc.CO2NET;
using Senparc.CO2NET.RegisterServices;
using Senparc.Weixin;
using Senparc.Weixin.Entities;
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
            Fd();
        }

        /// <summary>
        /// 注入
        /// </summary>
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



        public void Fd()
        {
            /* CO2NET 全局注册开始
             * 建议按照以下顺序进行注册
             */

            //设置全局 Debug 状态
            var isGLobalDebug = true;
            var senparcSetting = SenparcSetting.BuildFromWebConfig(isGLobalDebug);

            //CO2NET 全局注册，必须！！
            IRegisterService register = RegisterService.Start(senparcSetting)
                                          .UseSenparcGlobal(false, null);

            /* 微信配置开始
             * 建议按照以下顺序进行注册
             */

            //设置微信 Debug 状态
            var isWeixinDebug = true;
            var senparcWeixinSetting = SenparcWeixinSetting.BuildFromWebConfig(isWeixinDebug);

            //微信全局注册，必须！！
            register.UseSenparcWeixin(senparcWeixinSetting, senparcSetting);

        }

    }
}
