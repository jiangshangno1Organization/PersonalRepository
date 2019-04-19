using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebFinalApi.CustomException
{
    /// <summary>
    /// 验证异常
    /// </summary>
    public class VerificationException :  ApplicationException
    {
        //public MyException (){}
        public VerificationException(string message) : base(message)
        { }//这句话知道是干的吧?别和我说你忘了!!
        public override string Message
        {
            get
            {
                return base.Message;
            }
        }
    }

    /// <summary>
    /// 操作执行异常
    /// </summary>
    public class OperationException : ApplicationException
    {
        //public MyException (){}
        public OperationException(string message) : base(message)
        { }//这句话知道是干的吧?别和我说你忘了!!
        public override string Message
        {
            get
            {
                return base.Message;
            }
        }
    }


}