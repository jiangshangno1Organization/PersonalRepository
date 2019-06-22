using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebFinalApi.Empty;
using WebFinalApi.Models.Goods;

namespace WebFinalApi.Service
{
    public interface IGoodsService
    {

        /// <summary>
        /// 获取所有商品
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        GoodsDataOutput GetAllGoods();

        /// <summary>
        /// 获取商品详情
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        GoodsDetailOutput GetGoodsDetail(int ID);


        GoodsDataOutput GetGoodsByCategoryCD(string categoryCD);
       
    }
}