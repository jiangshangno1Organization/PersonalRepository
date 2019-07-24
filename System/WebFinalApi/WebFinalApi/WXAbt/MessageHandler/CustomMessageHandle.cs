using Senparc.NeuChar.Entities;
using Senparc.Weixin.MP.Entities.Request;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.MessageHandlers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using WebFinalApi.Helper;

namespace WebFinalApi.WXAbt.MessageHandler
{
    public class CustomMessageHandler : MessageHandler<CustomMessageContext>
    {
        public CustomMessageHandler(Stream inputStream, PostModel postModel)
          : base(inputStream, postModel)
        {

        }

        public override IResponseMessageBase DefaultResponseMessage(IRequestMessageBase requestMessage)
        {
            var responseMessage = base.CreateResponseMessage<ResponseMessageText>(); //ResponseMessageText也可以是News等其他类型
            responseMessage.Content = "这条消息来自DefaultResponseMessage。";
            return responseMessage;
        }

        public override IResponseMessageBase OnTextRequest(RequestMessageText requestMessage)
        {
            // string answer = GetAnswer(requestMessage.Content);
            var responseMessage = base.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "略略略";
            //responseMessage.Content = "您的OpenID是：" + requestMessage.FromUserName      //这里的requestMessage.FromUserName也可以直接写成base.WeixinOpenId
            //                              + "。\r\n您发送了文字信息：" + requestMessage.Content + "\r\n机器人回答：" + answer;  //\r\n用于换行，requestMessage.Content即用户发过来的文字内容
            return responseMessage;
        }

        private string GetAnswer(string question)
        {
            return RequestHelp.dd("http://openapi.tuling123.com/openapi/api/v2", question);
        }

    }
}