using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebFinalApi.Models.Order
{
    public class OrderSubmitDto
    {
        public string remindMsg { get; set; }

        public bool ifSuccess { get; set; }

        public int baseOrderID { get; set; }
    }

    public class OrderSubmitInputDto
    {
        public List<int> IDs { get; set; }

        public bool ifSumbitAll { get; set; }

    }

    
}