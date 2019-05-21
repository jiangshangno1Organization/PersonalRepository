using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using WebFinalApi.Helper;
using Senparc;
namespace WebFinalApi.Controllers
{
    using Newtonsoft.Json;
    using Senparc.Weixin;
    using Senparc.Weixin.MP;
    using Senparc.Weixin.MP.CommonAPIs;
    using Senparc.Weixin.MP.Containers;
    using Senparc.Weixin.MP.Entities.Menu;
    using Senparc.Weixin.MP.Entities.Request;
    using Senparc.Weixin.MP.MvcExtension;
    using System.Web.Http.Results;
    using System.Web.UI.WebControls;
    using WebFinalApi.WXAbt.MessageHandler;
    //using Senparc.Weixin.MP.Sample.CommonService.CustomMessageHandler;

    public class WeChatController : BaseController
    {
        public static readonly string Token = Config.SenparcWeixinSetting.Token;//与微信公众账号后台的Token设置保持一致，区分大小写。
        public static readonly string EncodingAESKey = Config.SenparcWeixinSetting.EncodingAESKey;//与微信公众账号后台的EncodingAESKey设置保持一致，区分大小写。
        public static readonly string AppId = Config.SenparcWeixinSetting.WeixinAppId;//与微信公众账号后台的AppId设置保持一致，区分大小写。
        public static readonly string AppSecret = Config.SenparcWeixinSetting.WeixinAppSecret;//与微信公众账号后台的AppId设置保持一致，区分大小写。

        private PostModel DataInit(PostModel postModel)
        {
            if (postModel == null)
            {
                postModel = new PostModel();
            }
            postModel.Token = Token;
            postModel.EncodingAESKey = EncodingAESKey;
            postModel.AppId = AppId;
            return postModel;
        }

        /// <summary>
        /// 微信后台验证地址（使用Get），微信后台的“接口配置信息”的Url填写如：http://weixin.senparc.com/weixin
        /// </summary>
        [HttpGet]
        [ActionName("weChat")]
        public void Anip(String signature, String timestamp, String nonce, String echostr)
        {
            PostModel postModel = new PostModel()
            {
                Signature = signature,
                Timestamp = timestamp,
                Nonce = nonce
            };
            postModel = DataInit(postModel);
            NetLog.WriteTextLog(" 11: ");
            NetLog.WriteTextLog(JsonConvert.SerializeObject(postModel) + ",  r:" + echostr);
            string res = string.Empty;
            try
            {
                if (CheckSignature.Check(postModel.Signature, postModel))
                {
                    res = echostr;
                    HttpContext.Current.Response.Write(echostr);
                }
                else
                {
                    res = @"failed:" + postModel.Signature + "," + CheckSignature.GetSignature(postModel.Timestamp, postModel.Nonce, Token) + "。" +
                      "如果你在浏览器中看到这句话，说明此地址可以被作为微信公众账号后台的Url，请注意保持Token一致。";
                    HttpContext.Current.Response.Write("failed:" + postModel.Signature + "," + CheckSignature.GetSignature(postModel.Timestamp, postModel.Nonce, Token) + "。" +
                      "如果你在浏览器中看到这句话，说明此地址可以被作为微信公众账号后台的Url，请注意保持Token一致。");
                }
            }
            catch (Exception ex)
            {
                NetLog.WriteTextLog(ex.Message);
            }
            NetLog.WriteTextLog(res);
            HttpContext.Current.Response.StatusCode = 200;
            HttpContext.Current.Response.End();
        }


        [HttpGet]
        [ActionName("weChat1")]
        public void Validate(String signature, String timestamp, String nonce, String echostr)
        {
            NetLog.WriteTextLog(" 22: ");
            if (CheckSignature1(Token, signature, timestamp, nonce))
            {
                NetLog.WriteTextLog($"{signature} {timestamp} {nonce} {echostr} RES : TRUE");
                HttpContext.Current.Response.Write(echostr);
                HttpContext.Current.Response.StatusCode = 200;
            }
            else
            {
                NetLog.WriteTextLog($"{signature} {timestamp} {nonce} {echostr} RES : NULL");
            }
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 验证微信签名
        /// </summary>
        /// * 将token、timestamp、nonce三个参数进行字典序排序
        /// * 将三个参数字符串拼接成一个字符串进行sha1加密
        /// * 开发者获得加密后的字符串可与signature对比，标识该请求来源于微信。
        /// <returns></returns>
        public bool CheckSignature1(string token, string signature, string timestamp, string nonce)
        {
            string[] ArrTmp = { token, timestamp, nonce };
            Array.Sort(ArrTmp);     //字典排序
            string tmpStr = string.Join("", ArrTmp);
            tmpStr = Helper.CodeVerificationHelper.SHA1(tmpStr);
            tmpStr = tmpStr.ToLower();
            if (tmpStr == signature)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
                /// 用户发送消息后，微信平台自动Post一个请求到这里，并等待响应XML。
                /// PS：此方法为简化方法，效果与OldPost一致。
                /// v0.8之后的版本可以结合Senparc.Weixin.MP.MvcExtension扩展包，使用WeixinResult，见MiniPost方法。
                /// </summary>
        [HttpPost]
        [ActionName("weChat")]
        public void Post(String signature, String timestamp, String nonce, String openid)
        {
            PostModel postModel = new PostModel()
            {
                Signature = signature,
                Timestamp = timestamp,
                Nonce = nonce
            };
            postModel = DataInit(postModel);
            HttpContextBase context = (HttpContextBase)Request.Properties["MS_HttpContext"];//获取传统context 
            HttpRequestBase request = context.Request;//定义传统request对象
            NetLog.WriteTextLog(" 1 ");
            try
            {
                //!CheckSignature1(Token, postModel.Signature, postModel.Timestamp, postModel.Nonce)
                if (false)
                {
                    NetLog.WriteTextLog(" 3 A ");
                    HttpContext.Current.Response.Write("参数错误！");
                }
                else
                {
                    postModel.Token = Token;//根据自己后台的设置保持一致
                    postModel.EncodingAESKey = EncodingAESKey;//根据自己后台的设置保持一致
                    postModel.AppId = AppId;//根据自己后台的设置保持一致
                                            //自定义MessageHandler，对微信请求的详细判断操作都在这里面。
                    var messageHandler = new CustomMessageHandler(request.InputStream, postModel);//接收消息
                    messageHandler.Execute();//执行微信处理过程
                    NetLog.WriteTextLog(" 5 ");
                    HttpContext.Current.Response.Write(new FixWeixinBugWeixinResult(messageHandler));
                }
                HttpContext.Current.Response.StatusCode = 200;
                HttpContext.Current.Response.End();
            }
            catch (Exception ex)
            {
                NetLog.WriteTextLog($" 4{ex.Message} ");
            }
        }

        [HttpPost]
        [ActionName("weChat")]
        public void Post(PostModel postModel , String echostr)
        {
            postModel = DataInit(postModel);
            HttpContextBase context = (HttpContextBase)Request.Properties["MS_HttpContext"];//获取传统context 
            HttpRequestBase request = context.Request;//定义传统request对象
            NetLog.WriteTextLog(" 1 ");
            try
            {
                if (!CheckSignature.Check(postModel.Signature, postModel))
                {
                    NetLog.WriteTextLog(" 3 A ");
                    HttpContext.Current.Response.Write("参数错误！");
                }
                else
                {
                    postModel.Token = Token;//根据自己后台的设置保持一致
                    postModel.EncodingAESKey = EncodingAESKey;//根据自己后台的设置保持一致
                    postModel.AppId = AppId;//根据自己后台的设置保持一致
                                            //自定义MessageHandler，对微信请求的详细判断操作都在这里面。
                    var messageHandler = new CustomMessageHandler(request.InputStream, postModel);//接收消息
                    messageHandler.Execute();//执行微信处理过程
                    NetLog.WriteTextLog(" 5 ");
                    HttpContext.Current.Response.Write(new FixWeixinBugWeixinResult(messageHandler));
                }
                HttpContext.Current.Response.StatusCode = 200;
                HttpContext.Current.Response.End();
            }
            catch (Exception ex)
            {
                NetLog.WriteTextLog($" 4{ex.Message} ");
            }
        }

        [HttpGet]
        [ActionName("dd")]
        public void dd()
        {
            //125.105.97.120
            AccessTokenContainer.Register(AppId, AppSecret);
            //  var accesstoken = Senparc.Weixin.MP.CommonAPIs.CommonApi.GetToken(AppId, AppSecret);
            var accesstoken = AccessTokenContainer.TryGetAccessToken(AppId, AppSecret);
            //21_aBoVaAUgaWbzmZkclQJSRVN2HkBLJCny12kOxwumMMavDaKUWMecnYNz6Vwmol1pgvYkaOzF7zmpFh3puXhhIx3JpjrMQUWw24O-1lFul60G2wjCUmP1e4QcfKAVJYdAHAZNC
            // var  accesstoken1 = Senparc.Weixin.MP.CommonAPIs.CommonApi.GetToken(AppId, AppSecret);
            ButtonGroup bg = new ButtonGroup();
            //单击
            bg.button.Add(new SingleClickButton()
            {
                name = "单击测试",
                key = "OneClick"
                //type = ButtonType.click.ToString(),//默认已经设为此类型，这里只作为演示
            });

            //二级菜单
            var subButton = new SubButton()
            {
                name = "二级菜单"
            };
            subButton.sub_button.Add(new SingleClickButton()
            {
                key = "SubClickRoot_Text",
                name = "返回文本"
            });
            subButton.sub_button.Add(new SingleClickButton()
            {
                key = "SubClickRoot_News",
                name = "返回图文"
            });
            subButton.sub_button.Add(new SingleClickButton()
            {
                key = "SubClickRoot_Music",
                name = "返回音乐"
            });
            subButton.sub_button.Add(new SingleViewButton()
            {
                url = "http://weixin.senparc.com",
                name = "Url跳转"
            });
            bg.button.Add(subButton);

            var result = CommonApi.CreateMenu(accesstoken, bg);
            var results = CommonApi.GetMenu(accesstoken);
        }



    }

}