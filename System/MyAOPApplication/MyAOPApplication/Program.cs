using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyAOPApplication1;
using Microsoft.Practices.EnterpriseLibrary.PolicyInjection;

namespace MyAOPApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 一、静态代理(AOP_Static.cs)
            //try
            //{
            //    Order order = new Order() { Id = 1, Name = "lee", Count = 10, Price = 100.00, Desc = "订单测试" };
            //    IOrderProcessor orderprocessor = new OrderProcessorDecorator(new OrderProcessor());
            //    orderprocessor.Submit(order);
            //    Console.ReadLine();
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            #endregion

            #region 二、动态代理——使用.Net Remoting/RealProxy(AOP_Dynamic1.cs)
            //try
            //{
            //    User user = new User() { Name = "lee", PassWord = "123123123123" };
            //    UserProcessor userprocessor = TransparentProxy.Create<UserProcessor>();
            //    userprocessor.RegUser(user);
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            #endregion

            #region 三、动态代理——使用EnterPrise Lib实现拦截(AOP_Dynamic2.cs)
            try
            {
                var oUserTest1 = new User() { Name = "test2222", PassWord = "yxj" };
                var oUserTest2 = new User() { Name = "test3333", PassWord = "yxj" };
                var oUser = UserOperation.GetInstance();
                oUser.Test(oUserTest1);
                oUser.Test2(oUserTest1,oUserTest2);
            }
            catch (Exception ex)
            {

                //throw;
            }
            #endregion
        }


    }
}
