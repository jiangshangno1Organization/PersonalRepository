using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebFinalApi.Empty
{
    public class CodeVerification
    {
        public int id { get; set; }
        public string values { get; set; }
        public string ifSend { get; set; }
        public string status { get; set; }
        public DateTime initTime { get; set; }
        public DateTime sendTime { get; set; }
        public string type { get; set; }
        public int failtime { get; set; }
    }
}