using AFinalDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebFinalApi.CustomException;
using WebFinalApi.Empty;
using WebFinalApi.Models.Goods;

namespace WebFinalApi.Service
{
    public class GoodsService : BaseService, IGoodsService
    {

        public GoodsService(CommonDB dB) : base(dB)
        {
        }

        /// <summary>
        /// 获取所有商品
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public new GoodsDataOutput GetAllGoods()
        {
            //基础信息
            var goodsData = base.GetAllGoods();
            return GenerateGoods(goodsData.Select(i=>i.goodsID));
        }

        /// <summary>
        /// 获取商品 通过categoryCD
        /// </summary>
        /// <param name="categoryCD"></param>
        /// <returns></returns>
        public GoodsDataOutput GetGoodsByCategoryCD(string categoryCD)
        {
            //获取分类信息 
            var category = GetCategory(categoryCD);
            if (category == null)
            {
                throw new VerificationException("验证码错误");
            }
            //获取分类下商品信息
            var goods = GetGoodsByCategoryIDs(new List<string>() { categoryCD });
            //无商品 正常 return
            if (goods == null)
            {
                return null;
            }
            return GenerateGoods(goods.Select(i => i.goodsID));
        }
    }
}