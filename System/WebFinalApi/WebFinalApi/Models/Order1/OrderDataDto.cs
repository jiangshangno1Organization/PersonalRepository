using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebFinalApi.Models.Order
{
    public class OrderDataDto
    {
        public string orderDate { get; set; }
        public string orderStatus { get; set; }
        public int baseID { get; set; }

        public string address { get; set; }

        public string mobile { get; set; }

        public string userName { get; set; }

        public decimal sum { get; set; }

        public decimal factSum { get; set; }
        
        public List<OrderDetailDto> orderDetails { get; set; }

        public int goodsNumber { get; set; }
    }


    public class OrderDetailDto
    {
        public int baseID { get; set; }

        public int goodsID { get; set; }

        public string goodsCD { get; set; }

        public string goodsName { get; set; }

        public decimal unitPrice { get; set; }

        public decimal allPrice { get; set; }

        public int goodsCount { get; set; }


        public string goodsPic { get; set; }
    }


}