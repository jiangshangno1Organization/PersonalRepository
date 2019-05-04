using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebFinalApi.CustomException;
using WebFinalApi.Filter;
using WebFinalApi.Models;
using WebFinalApi.Models.Order;
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

        #region 购物车相关
   
        /// <summary>
        /// 获取购物车
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [HttpGet]
        [AuthorizationFilter]
        public BaseResponseModel<OrderCartDto> GetUserCart(int userID)
        {
            return ResponsePack.Responsing(orderService.GetUserCart(userID));
        }

        /// <summary>
        /// 添加到购物车
        /// </summary>
        /// <param name="addCart"></param>
        /// <returns></returns>
        [HttpPut]
        [AuthorizationFilter]
        public BaseResponseModel<bool> AddToCart(AddCartInput addCart)
        {
            return ResponsePack.Responsing(orderService.ADDToCart(userDataContent.userId, addCart.goodsID, addCart.goodsCount));
        }

        #endregion

        #region 订单相关

        /// <summary>
        /// 订单提交
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="IDs"></param>
        /// <param name="ifSumbitAll"></param>
        /// <returns></returns>
        [HttpPut]
        [AuthorizationFilter]
        public BaseResponseModel<OrderSubmitDto> SubmitOrderByCart(List<int> IDs, bool ifSumbitAll = false)
        {
            OrderSubmitDto submitDto = new OrderSubmitDto();
            string errMsg = string.Empty;
            try
            {
                submitDto = orderService.SubmitOrderByCart(userDataContent.userId, IDs, ifSumbitAll);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return ResponsePack.Responsing(submitDto, errMsg);
        }

        /// <summary>
        /// 订单支付
        /// </summary>
        /// <returns></returns>
        [AuthorizationFilter]
        public void PayOrder(int orderID)
        {

        }

        #endregion

        #region 订单数据获取

        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpGet]
        [AuthorizationFilter]
        public BaseResponseModel<List<OrderDataDto>> GetGetOrderList(string type)
        {
            return ResponsePack.Responsing(orderService.GetOrderList(userDataContent.userId, type));
        }

        #endregion
    }
}