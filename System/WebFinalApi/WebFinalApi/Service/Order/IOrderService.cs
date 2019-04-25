using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFinalApi.Empty;

namespace WebFinalApi.Service
{
    public interface IOrderService
    {

        /// <summary>
        /// 获取用户购物车
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        IEnumerable<OrderCart> GetUserCart(int userID);

        /// <summary>
        /// 订单提交
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="IDs"></param>
        /// <param name="ifSumbitAll"></param>
        /// <returns></returns>
        int SubmitOrderByCart(int userID, List<int> IDs, bool ifSumbitAll = false);

        /// <summary>
        /// 订单支付
        /// </summary>
        /// <returns></returns>
        int PayOrder(int orderID);
    }
}
