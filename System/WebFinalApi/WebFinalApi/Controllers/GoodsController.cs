using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebFinalApi.CustomException;
using WebFinalApi.Models;
using WebFinalApi.Models.Goods;
using WebFinalApi.Service;

namespace WebFinalApi.Controllers
{
    public class GoodsController : BaseController
    {
        private readonly IGoodsService goodsService;

        public GoodsController(IGoodsService goods)
        {
            goodsService = goods;
        }

        /// <summary>
        /// 获取商品数据
        /// </summary>
        /// <returns></returns>
        public BaseResponseModel<GoodsDataOutput> GetAllGoods()
        {
            string errMsg = string.Empty;
            GoodsDataOutput result = null;
            try
            {
                result = goodsService.GetAllGoods();
            }
            catch (VerificationException ex)
            {
                errMsg = ex.Message;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return ResponsePack.Responsing(result, errMsg);
        }

        /// <summary>
        /// 获取商品数据 通过分类CD
        /// </summary>
        /// <param name="categoryCD"></param>
        /// <returns></returns>
        public BaseResponseModel<GoodsDataOutput> GetGoodsByCategoryCD(string categoryCD)
        {
            string errMsg = string.Empty;
            GoodsDataOutput result = null;
            try
            {
                result = goodsService.GetGoodsByCategoryCD(categoryCD);
            }
            catch (VerificationException ex)
            {
                errMsg = ex.Message;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return ResponsePack.Responsing(result , errMsg);
        }

    }
}