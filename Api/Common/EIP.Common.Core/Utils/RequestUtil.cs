using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using Newtonsoft.Json;

namespace EIP.Common.Core.Utils
{
    /// <summary>
    /// 请求工具
    /// </summary>
    public static class RequestUtil
    {
        /// <summary>
        ///     请求与响应的超时时间
        /// </summary>
        public static int Timeout { get; set; } = 100000;

        /// <summary>
        ///     执行HTTP POST请求。
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="parameters">请求参数</param>
        /// <param name="charset">编码字符集</param>
        /// <returns>HTTP响应</returns>
        public static string DoPost(string url, IDictionary<string, string> parameters, string charset = "utf-8")
        {
            var req = GetWebRequest(url, "POST");
            req.ContentType = "application/x-www-form-urlencoded;charset=" + charset;
            var postData = Encoding.GetEncoding(charset).GetBytes(BuildQuery(parameters, charset));
            var reqStream = req.GetRequestStream();
            reqStream.Write(postData, 0, postData.Length);
            reqStream.Close();

            var rsp = (HttpWebResponse)req.GetResponse();
            var encoding = Encoding.GetEncoding(rsp.CharacterSet);
            return GetResponseAsString(rsp, encoding);
        }

        /// <summary>
        ///     执行HTTP POST请求。
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="data">请求参数:</param>
        /// <param name="charset">编码字符集</param>
        /// <returns>HTTP响应</returns>
        public static string DoPost(string url, object data, string charset = "utf-8")
        {
            var req = GetWebRequest(url, "POST");
            req.ContentType = "application/x-www-form-urlencoded;charset=" + charset;
            var postData = Encoding.GetEncoding(charset).GetBytes(data == null ? string.Empty : JsonConvert.SerializeObject(data));
            var reqStream = req.GetRequestStream();
            reqStream.Write(postData, 0, postData.Length);
            reqStream.Close();

            var rsp = (HttpWebResponse)req.GetResponse();
            var encoding = Encoding.GetEncoding(rsp.CharacterSet);
            return GetResponseAsString(rsp, encoding);
        }
        /// <summary>
        ///     执行HTTP GET请求。
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="parameters">请求参数</param>
        /// <param name="charset">编码字符集</param>
        /// <returns>HTTP响应</returns>
        public static string DoGet(string url, IDictionary<string, string> parameters, string charset = "utf-8")
        {
            if (parameters != null && parameters.Count > 0)
                if (url.Contains("?"))
                    url = url + "&" + BuildQuery(parameters, charset);
                else
                    url = url + "?" + BuildQuery(parameters, charset);

            var req = GetWebRequest(url, "GET");
            req.ContentType = "application/x-www-form-urlencoded;charset=" + charset;
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var rsp = (HttpWebResponse)req.GetResponse();
            var encoding = Encoding.GetEncoding(rsp.CharacterSet);
            return GetResponseAsString(rsp, encoding);
        }



        public static HttpWebRequest GetWebRequest(string url, string method)
        {
            var req = (HttpWebRequest)WebRequest.Create(url);
            //			req.ServicePoint.Expect100Continue = false;
            req.Method = method;
            req.KeepAlive = true;
            req.UserAgent = "Aop4Net";
            req.Timeout = Timeout;
            return req;
        }

        /// <summary>
        ///     把响应流转换为文本。
        /// </summary>
        /// <param name="rsp">响应流对象</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>响应文本</returns>
        public static string GetResponseAsString(HttpWebResponse rsp, Encoding encoding)
        {
            var result = new StringBuilder();
            Stream stream = null;
            StreamReader reader = null;

            try
            {
                // 以字符流的方式读取HTTP响应
                stream = rsp.GetResponseStream();
                reader = new StreamReader(stream, encoding);

                // 按字符读取并写入字符串缓冲
                var ch = -1;
                while ((ch = reader.Read()) > -1)
                {
                    // 过滤结束符
                    var c = (char)ch;
                    if (c != '\0')
                        result.Append(c);
                }
            }
            finally
            {
                // 释放资源
                if (reader != null) reader.Close();
                if (stream != null) stream.Close();
                if (rsp != null) rsp.Close();
            }

            return result.ToString();
        }

        /// <summary>
        ///     组装普通文本请求参数。
        /// </summary>
        /// <param name="parameters">Key-Value形式请求参数字典</param>
        /// <param name="charset"></param>
        /// <returns>URL编码后的请求数据</returns>
        public static string BuildQuery(IDictionary<string, string> parameters, string charset)
        {
            if (string.IsNullOrEmpty(charset))
                charset = "utf-8";
            var postData = new StringBuilder();
            var hasParam = false;

            var dem = parameters.GetEnumerator();
            while (dem.MoveNext())
            {
                var name = dem.Current.Key;
                var value = dem.Current.Value;
                // 忽略参数名或参数值为空的参数
                if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(value))
                {
                    if (hasParam)
                        postData.Append("&");

                    postData.Append(name);
                    postData.Append("=");

                    var encodedValue = HttpUtility.UrlEncode(value, Encoding.GetEncoding(charset));

                    postData.Append(encodedValue);
                    hasParam = true;
                }
            }

            return postData.ToString();
        }
    }

}