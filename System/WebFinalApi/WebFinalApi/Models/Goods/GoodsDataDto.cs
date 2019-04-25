using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebFinalApi.Models.Goods
{
    public class GoodsDataOutput
    {
        public List<GoodsCell> goods { get; set; }

        public int goodsCount { get; set; }
    }


    public class GoodsCell
    {
        public int goodsID { get; set; }
        public string goodsName { get; set; }
        public string goodsCD { get; set; }
        public decimal price { get; set; }
        public decimal aPrice { get; set; }
        public string text1 { get; set; }
        public string text2 { get; set; }
        public int classID { get; set; }

        public List<GoodsPictrure> goodsPictrures { get; set; }
    }

    public class GoodsPictrure
    {
        public string file { get; set; }
        public string key { get; set; }
    }

}