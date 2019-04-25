using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebFinalApi.Filter;
using WebFinalApi.Service;

namespace WebFinalApi.Controllers
{
    public class OrderController: BaseController
    {
        private readonly IOrderService orderService;
        public OrderController(OrderService order)
        {
            orderService = order;
        }

        [AuthorizationFilter]
        public void GetUserCart(int userID)
        {

        }

        /// <summary>
        /// 订单提交
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="IDs"></param>
        /// <param name="ifSumbitAll"></param>
        /// <returns></returns>
        [AuthorizationFilter]
        public void SubmitOrderByCart(int userID, List<int> IDs, bool ifSumbitAll = false)
        {

        }

        /// <summary>
        /// 订单支付
        /// </summary>
        /// <returns></returns>
        [AuthorizationFilter]
        public void PayOrder(int orderID)
        {

        }


    }
}