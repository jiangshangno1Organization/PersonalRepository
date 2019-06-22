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

        #region 购物车相关

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

        bool RemoveCart(int userID, int gdsID, bool ifRemoveAll = false);

        #endregion

        #region 订单相关

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
        /// 生效订单
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        bool EffectiveOrder(int orderID);

        #endregion

        #region 订单数据获取

        /// <summary>
        /// 获取订单列表（0：未付款 1：未收货 2：已完成 3:已取消）
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        List<OrderDataDto> GetOrderList(int userID, string type);

        /// <summary>
        /// 获取订单基础数据
        /// </summary>
        /// <param name="baseID"></param>
        /// <returns></returns>
        OrderDataDto GetOrderDetail(int baseID);

        #endregion

    }
}
