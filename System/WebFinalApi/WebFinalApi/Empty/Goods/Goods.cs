using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebFinalApi.Empty
{
    public class Goods
    {
        public int goodsId { get; set; }
        public string goodsName { get; set;}
        public int classId { get; set; }
        public string goodsCd { get; set; }
        public decimal price { get; set; }
        public decimal aPrice { get; set; }
        public string ifdel { get; set; }
        public string text1 { get; set; }
        public string text2 { get; set; }
    }
}