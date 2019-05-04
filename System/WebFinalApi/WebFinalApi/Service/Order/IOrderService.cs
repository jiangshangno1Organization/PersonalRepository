using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFinalApi.Empty;
using WebFinalApi.Models.Order;

namespace WebFinalApi.Service
{
    public interface IOrderService
    {

        /// <summary>
        /// 获取用户购物车
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        OrderCartDto GetUserCart(int userID);

        /// <summary>
        /// 添加到购物车
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="gdsID"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        bool ADDToCart(int userID, int gdsID, int count);

        /// <summary>
        /// 订单提交
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="IDs"></param>
        /// <param name="ifSumbitAll"></param>
        /// <returns></returns>
        OrderSubmitDto SubmitOrderByCart(int userID, List<int> IDs, bool ifSumbitAll = false);

        /// <summary>
        /// 订单支付
        /// </summary>
        /// <returns></returns>
        int PayOrder(int orderID);

        /// <summary>
        /// 获取订单列表（0：未付款 1：未收货 2：已完成）
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        List<OrderDataDto> GetOrderList(int userID, string type);
    }
}
