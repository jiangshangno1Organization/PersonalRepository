using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebFinalApi.Empty;

namespace WebFinalApi.Models.Order
{
    public class OrderCartDto
    {
        public List<OrderCart> orderCarts { get; set; }
    }

    public class AddCartInput
    {

        public int goodsID { get; set; }

        public int goodsCount { get; set; }

    }
}