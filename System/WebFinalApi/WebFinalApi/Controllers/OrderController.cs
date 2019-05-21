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
        public BaseResponseModel<OrderCartDto> GetUserCart()
        {
            return ResponsePack.Responsing(orderService.GetUserCart(userDataContent.userID));
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
            return ResponsePack.Responsing(orderService.ADDToCart(userDataContent.userID, addCart.goodsID, addCart.goodsCount));
        }

        /// <summary>
        /// 购物车删除
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [AuthorizationFilter]
        public BaseResponseModel<bool> RemoveCart(RemoveCartInput removeCartInput)
        {
            return ResponsePack.Responsing(orderService.RemoveCart(userDataContent.userID, removeCartInput.goodsID, removeCartInput.ifRemoveAll));
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
        public BaseResponseModel<OrderSubmitDto> SubmitOrderByCart(OrderSubmitInputDto orderSubmitInputDto)
        {
            OrderSubmitDto submitDto = new OrderSubmitDto();
            string errMsg = string.Empty;
            try
            {
                submitDto = orderService.SubmitOrderByCart(userDataContent.userID, orderSubmitInputDto.IDs, orderSubmitInputDto.ifSumbitAll);
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
            return ResponsePack.Responsing(orderService.GetOrderList(userDataContent.userID, type));
        }

        /// <summary>
        /// 订单详情数据获取
        /// </summary>
        /// <param name="baseID"></param>
        /// <returns></returns>
        [HttpGet]
        [AuthorizationFilter]
        public BaseResponseModel<OrderDataDto> GetOrderDetail(int baseID)
        {
            return ResponsePack.Responsing(orderService.GetOrderDetail(baseID));
        }

        #endregion
    }
}