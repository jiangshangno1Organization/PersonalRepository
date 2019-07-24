using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace WebFinalApi.Helper
{
    public class RequestHelp
    {
        /// 发送请求  
        /// </summary>  
        /// <param name="url">请求地址</param>  
        /// <param name="sendData">参数格式 “name=王武&pass=123456”</param>  
        /// <returns></returns>  
        public static string RequestWebAPI(string url, string sendData)
        {
            string backMsg = "";
            try
            {
                System.Net.WebRequest httpRquest = System.Net.HttpWebRequest.Create(url);
                httpRquest.Method = "POST";
                //这行代码很关键，不设置ContentType将导致后台参数获取不到值  
                httpRquest.ContentType = "application/json;charset=UTF-8";
                byte[] dataArray = System.Text.Encoding.UTF8.GetBytes(sendData);
                //httpRquest.ContentLength = dataArray.Length;  
                System.IO.Stream requestStream = null;
                if (string.IsNullOrWhiteSpace(sendData) == false)
                {
                    requestStream = httpRquest.GetRequestStream();
                    requestStream.Write(dataArray, 0, dataArray.Length);
                    requestStream.Close();
                }
                System.Net.WebResponse response = httpRquest.GetResponse();
                System.IO.Stream responseStream = response.GetResponseStream();
                System.IO.StreamReader reader = new System.IO.StreamReader(responseStream, System.Text.Encoding.UTF8);
                backMsg = reader.ReadToEnd();
                reader.Close();
                reader.Dispose();

                requestStream.Dispose();
                responseStream.Close();
                responseStream.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return backMsg;
        }

        public static string dd(string url, string data)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"user\":\"test\"," +
                              "\"password\":\"bla\"}";

                json = " {\"reqType\":0," +
                       " \"perception\": {" +
                       " \"inputText\": { " +
                       " \"text\": \"" + data + "\" " +
                       "  } } ," +
                       " \"userInfo\": { " +
                       " \"apiKey\": \"af3851db8acf4ba38924896785a4f43d\"," +
                       " \"userId\": \"\"" +
                             " } }";

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }
            string res = "";
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                res = Convert.ToString(result);
            }
            return res;
        }


    }
}