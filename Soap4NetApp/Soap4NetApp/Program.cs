namespace Soap4NetApp
{
    #region Using

    using System;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Text;

    #endregion

    class Program
    {
        static void Main(string[] args)
        {
            if (false)
            {
                var webRequest = (HttpWebRequest) WebRequest.Create("http://localhost:11211/WebService1.asmx");

                //                var webRequest = (HttpWebRequest)WebRequest.Create("http://localhost:11211/WebService1.asmx/HelloWorld2");
                webRequest.Headers.Add("SOAPAction", "http://ceshi.com/HelloWorld");

                //                webRequest.ContentType = "application/x-www-form-urlencoded";
                //                webRequest.ContentType = "text/xml;charset=\"utf-8\"";
                webRequest.ContentType = "text/xml";

                //                webRequest.Accept = "text/xml";
                webRequest.Method = "POST";

                StringBuilder soap = new StringBuilder();

                //                soap.Append("abc=lalala");

                /*soap.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                soap.Append("<abc xmlns=\"http://ceshi.com/\">lalala</abc>");*/

                soap.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                soap.Append("<soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsd=\"http://ceshi.com/\">");
                soap.Append("<soap:Body>");
                soap.Append("<HelloWorld2 xmlns=\"http://ceshi.com/\">");
                soap.Append("<abc>lalala</abc>");
                soap.Append("</HelloWorld2>");
                soap.Append("</soap:Body>");
                soap.Append("</soap:Envelope>");

                // 写入请求SOAP信息
                using (var requestStream = webRequest.GetRequestStream())
                {
                    byte[] paramBytes = Encoding.UTF8.GetBytes(soap.ToString());
                    requestStream.Write(paramBytes, 0, paramBytes.Length);
                }

                // 获取SOAP请求返回
                //响应
                WebResponse webResponse = webRequest.GetResponse();
                using (StreamReader myStreamReader = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8))
                {
                    Console.WriteLine(myStreamReader.ReadToEnd());

                    //                    Debug.WriteLine(myStreamReader.ReadToEnd());
                }
            }
            else
            {
                HttpClient httpclient = new HttpClient();

                //HttpRequestMessage message = new HttpRequestMessage(new HttpMethod("POST"), "http://localhost:11211/WebService1.asmx");

                var webRequest = (HttpWebRequest) WebRequest.Create("http://localhost:11211/WebService1.asmx");

                //webRequest.Headers.Add("SOAPAction", "http://ceshi.com/HelloWorld2");

                //            webRequest.Headers.Add("SOAPAction", "http://ceshi.com/");
                webRequest.ContentType = "text/xml;charset=\"utf-8\"";
                webRequest.Accept = "text/xml";
                webRequest.Method = "POST";

                //构造soap请求信息
                StringBuilder soap = new StringBuilder();
                soap.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                soap.Append("<soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsd=\"http://ceshi.com/\">");
                soap.Append("<soap:Body>");
                soap.Append("<HelloWorld2 xmlns=\"http://ceshi.com/\">");
                soap.Append("<abc>aaa</abc>");
                soap.Append("</HelloWorld2>");
                soap.Append("</soap:Body>");
                soap.Append("</soap:Envelope>");

                //message.Content = new StringContent(soap.ToString(), Encoding.UTF8);
                //message.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("text/xml");

                //var x = httpclient.SendAsync(message).Result;

                StringContent content = new StringContent(soap.ToString(), Encoding.UTF8, "text/xml");
                content.Headers.Add("SOAPAction", "http://ceshi.com/HelloWorld2");

                string x = httpclient.PostAsync("http://localhost:11211/WebService1.asmx", content).Result.Content.ReadAsStringAsync().Result;

                // 写入请求SOAP信息
                using (var requestStream = webRequest.GetRequestStream())
                {
                    byte[] paramBytes = Encoding.UTF8.GetBytes(soap.ToString());
                    requestStream.Write(paramBytes, 0, paramBytes.Length);
                }

                // 获取SOAP请求返回
                //响应
                WebResponse webResponse = webRequest.GetResponse();
                using (StreamReader myStreamReader = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8))
                {
                    Console.WriteLine(myStreamReader.ReadToEnd());
                }
            }
        }
    }
}