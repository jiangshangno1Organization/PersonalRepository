using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebFinalApi.Models.Goods;

namespace WebFinalApi.Models.Order
{
    public class OrderCartDto
    {
        public List<CarCell> orderCarts { get; set; }
    }

    /// <summary>
    /// 购物车cell
    /// </summary>
    public class CarCell
    {
        /// <summary>
        /// 商品信息
        /// </summary>
        public GoodsCell goodsCell { get; set; }
        /// <summary>
        /// 购物车ID
        /// </summary>
        public int carID { get; set; }
        /// <summary>
        /// 商品数据
        /// </summary>
        public int goodsCount { get; set; }
        public string memo { get; set; }
    }

    /// <summary>
    /// 购物车添加
    /// </summary>
    public class AddCartInput
    {

        public int goodsID { get; set; }

        public int goodsCount { get; set; }
    }

    /// <summary>
    /// 购物车删除
    /// </summary>
    public class RemoveCartInput
    {
        public int cartID { get; set; }
        public int goodsID { get; set; }
        public bool ifRemoveAll { get; set; }
    }
         
}