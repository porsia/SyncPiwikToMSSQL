using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.Linq;
using System.Web;
using System.Collections;
using System.Collections.Specialized;

using Newtonsoft.Json;

namespace RunPiwikData
{
    class Program
    {
        public static string DecodeUrlAll(string source_url)
        {
            string curl = "";

            try
            {
                //curl = MyUrlDeCode(source_url);
                if (curl == "")
                {
                    if (!source_url.Contains(@"=\u"))
                        curl = new Uri(source_url).ToString();
                }
              // curl=System.Web.HttpUtility.UrlDecode(source_url, System.Text.Encoding.GetEncoding("GB2312"));

               // byte[] bt = Encoding.UTF8.GetBytes(source_url);
               // MemoryStream stm = new MemoryStream(bt);
               // StreamReader stmr = new StreamReader(stm);
               // string url = stmr.ReadToEnd();


               //curl=new Uri(url).ToString();

               
                
            }
            catch
            {
                curl="";
            }

            return curl;
        }

        /// <summary>
        /// 获取Url的参数,不编码,只获取明码
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private static string GetUrlParam(string query ,string key)
        {
              
                if (query != null && query.Length > 0)
                {
                    int index = 0;
                    index = query.IndexOf(key + "=");
                    if (index >= 0)
                    {
                        query = query.Substring(key.Length + 1 + index);
                        index = query.IndexOf('&');
                        if (index >= 0)
                            query = query.Substring(0, index);
                        return query;
                    }
               
              }
            return string.Empty;
        }


        /// <summary>
        /// 解码URL.
        /// </summary>
        /// <param name="encoding">null为自动选择编码</param>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string MyUrlDeCode(string str, Encoding encoding)
        {
        

            if (string.IsNullOrEmpty(str))
                return str;

            if (Tools.IsUTF8(str))
                encoding = Encoding.UTF8;
            else
                encoding = Encoding.GetEncoding("gb2312");
            return HttpUtility.UrlDecode(str, encoding);


            if (encoding == null)
            {
                Encoding utf8 = Encoding.UTF8;
                //首先用utf-8进行解码                     
                string code = HttpUtility.UrlDecode(str, utf8);

                if (str == code)
                    encoding = utf8;
                else
                {
                    //将已经解码的字符再次进行编码.
                    string encode = HttpUtility.UrlEncode(code, utf8);
                    if (str.ToUpper() == encode.ToUpper())
                        encoding = utf8;
                    else
                        encoding = Encoding.GetEncoding("gb2312");
                }
            }
            return HttpUtility.UrlDecode(str, encoding);
        }
        /// <summary>
        /// 将查询字符串解析转换为名值集合.
        /// </summary>
        /// <param name="queryString"></param>
        /// <returns></returns>
        public static NameValueCollection GetQueryString(string queryString)
        {
            return GetQueryString(queryString, null, true);
        }

        /// <summary>
        /// 将查询字符串解析转换为名值集合.
        /// </summary>
        /// <param name="queryString"></param>
        /// <param name="encoding"></param>
        /// <param name="isEncoded"></param>
        /// <returns></returns>
        public static NameValueCollection GetQueryString(string queryString, Encoding encoding, bool isEncoded)
        {
            queryString = queryString.Replace("?", "");
            NameValueCollection result = new NameValueCollection(StringComparer.OrdinalIgnoreCase);
            if (!string.IsNullOrEmpty(queryString))
            {
                int count = queryString.Length;
                for (int i = 0; i < count; i++)
                {
                    int startIndex = i;
                    int index = -1;
                    while (i < count)
                    {
                        char item = queryString[i];
                        if (item == '=')
                        {
                            if (index < 0)
                            {
                                index = i;
                            }
                        }
                        else if (item == '&')
                        {
                            break;
                        }
                        i++;
                    }
                    string key = null;
                    string value = null;
                    if (index >= 0)
                    {
                        key = queryString.Substring(startIndex, index - startIndex);
                        value = queryString.Substring(index + 1, (i - index) - 1);
                    }
                    else
                    {
                        key = queryString.Substring(startIndex, i - startIndex);
                    }
                    if (isEncoded)
                    {
                        result[MyUrlDeCode(key, encoding)] = MyUrlDeCode(value, encoding);
                    }
                    else
                    {
                        result[key] = value;
                    }
                    if ((i == (count - 1)) && (queryString[i] == '&'))
                    {
                        result[key] = string.Empty;
                    }
                }
            }
            return result;
        }

        public static string GetNewString(string queryString, Encoding encoding, bool isEncoded)
        {
            string newString = queryString;
            Uri uri = null;

            try
            {
                uri = new Uri(queryString);
            }
            catch
            {
                return newString;
            }
           queryString = uri.Query;


            queryString = queryString.Replace("?", "");
            NameValueCollection result = new NameValueCollection(StringComparer.OrdinalIgnoreCase);
            if (!string.IsNullOrEmpty(queryString))
            {
                int count = queryString.Length;
                for (int i = 0; i < count; i++)
                {
                    int startIndex = i;
                    int index = -1;
                    while (i < count)
                    {
                        char item = queryString[i];
                        if (item == '=')
                        {
                            if (index < 0)
                            {
                                index = i;
                            }
                        }
                        else if (item == '&')
                        {
                            break;
                        }
                        i++;
                    }
                    string key = null;
                    string value = null;
                    if (index >= 0)
                    {
                        key = queryString.Substring(startIndex, index - startIndex);
                        value = queryString.Substring(index + 1, (i - index) - 1);
                    }
                    else
                    {
                        key = queryString.Substring(startIndex, i - startIndex);
                    }
                    if (isEncoded)
                    {

                        newString=newString.Replace(key + "=" + value, key + "=" + MyUrlDeCode(value, encoding));
                        result[MyUrlDeCode(key, encoding)] = MyUrlDeCode(value, encoding);
                    }
                    else
                    {
                        result[key] = value;
                    }
                    if ((i == (count - 1)) && (queryString[i] == '&'))
                    {
                        result[key] = string.Empty;
                    }
                }
            }
            return newString;
        }

       

        static void Main(string[] args)
        {
            //string pp = "http://www.sogou.com/sogou?pid=Af11228-1464&query=\\u6bcd\\u5a74\\u4e4b\\u5bb6&p=50040111&sourceid=sugg&w=01015004&oq=muying&ri=1";

            //string pp = "http://www.soso.com/q?unc=i400044&sc=web&bs=%C0%D6%D3%D1&ch=w.uf&num=10&w=%C4%B8%D3%A4%D6%AE%BC%D2";

            //string pp = "http://www.baidu.com/s?wd=%E5%9F%BA%E8%AF%BA%E6%B5%A6%20%E4%BA%A7%E5%9C%B0&rsp=9&f=1&oq=%E6%83%A0%E6%AD%A5%E8%88%92%20%E5%9F%BA%E8%AF%BA%E6%B5%A6%20%E5%8E%82%E5%AE%B6&tn=srcindex_hao&ie=utf-8&rs_src=0";

            //string pp = "http://www.sogou.com/sogou?query=%C4%B8%D3%A4%D6%AE%BC%D2&pid=5F0EF-5842&iv=6.2.0.7270";
            //Uri u11 = new Uri(pp);
            //foreach (string uss in u11.Segments)
            //{
            //    Console.WriteLine(UrlDecode(uss));
            //}
            //Console.WriteLine(System.Web.HttpUtility.UrlDecode(pp, System.Text.Encoding.GetEncoding("GB2312")));
            //Console.WriteLine(System.Web.HttpUtility.UrlDecode(pp, System.Text.Encoding.UTF8));

            //Console.WriteLine(Microsoft.JScript.GlobalObject.decodeURI(pp));

            //byte[] bt = Encoding.UTF8.GetBytes(pp);
            //MemoryStream stm = new MemoryStream(bt);
            //StreamReader stmr = new StreamReader(stm);
            //string sssurl = stmr.ReadToEnd();
          

            // Console.WriteLine(MyUrlDeCode(pp,null));
            ////Console.ReadLine();


            ////string pageURL = "http://www.google.com.hk/search?hl=zh-CN&source=hp&q=%E5%8D%9A%E6%B1%87%E6%95%B0%E7%A0%81&aq=f&aqi=g2&aql=&oq=&gs_rfai=";


            //Console.WriteLine(GetNewString(pp, null, true));

            //Console.ReadLine();
           

            Dictionary<string, string> dicStrs = new Dictionary<string, string>();
            using (OffBBhomeDataContext ctx = new OffBBhomeDataContext())
            {
               // ctx.ObjectTrackingEnabled = false;
               // var log = ctx.piwik_log_reffers.Select(c => c.refferurl).Distinct().ToList();

                var log = ctx.piwik_log_reffers.Where(c=>c.refergbk==null).Distinct();
                          
                int count = 0;
                int total=log.Count();
                Console.WriteLine(total);
                foreach (piwik_log_reffer l in log)
                {

                    string tstr = l.refferurl.ToString();
                    string durl = tstr;
                    if (!string.IsNullOrEmpty(tstr) && tstr != "http://about:blank")
                        durl = GetNewString(tstr, null, true);

                    if ( tstr.Length > 0)
                    {
                      
                        dicStrs.Add(tstr, durl);

                        Console.WriteLine(l.Id + "," + durl);
                        l.refergbk = durl;
                        count++;


                      
                        //if (tstr != durl)
                        //{
                        //    Console.WriteLine(l.Id + "," + durl);
                        //    l.refergbk = durl;
                        //    count++;
                           
                        //}
                        //else
                        //    Console.WriteLine(l.Id);

                        if (count % 10000 == 0)
                        {
                            ctx.SubmitChanges();
                            string writeFile1 = string.Format("{0}\\result{1}.txt", System.Threading.Thread.GetDomain().BaseDirectory, DateTime.Now.ToFileTimeUtc().ToString());
                            writeLog(writeFile1, dicStrs);
                            dicStrs = new Dictionary<string, string>();
                         

                        }
                        if (count == total)
                        {
                            ctx.SubmitChanges();
                        }

                    }


                };
                ctx.SubmitChanges();
              

                writeLog(string.Format("{0}\\r{1}.txt", System.Threading.Thread.GetDomain().BaseDirectory, DateTime.Now.ToFileTimeUtc().ToString()), dicStrs);
                dicStrs = new Dictionary<string, string>();
            }

            Console.ReadLine();
            return;
            string dt = "2013-5-18";

            string token = "453170c79e8f0ad5dcd1f0b2ce1ecf23";
            //数据读取完毕退出

            Int64 maxVisitID = 0;

            try
            {
                for (int mi = 0; mi < int.MaxValue; mi++)
                {


                    #region 循环读取数据

                    // string url = "http://click.muyingzhijia.com/index.php?module=API&filter_limit=100&method=Live.getLastVisitsDetails&format=json&idSite=1&period=day&date=" + dtStr + "&expanded=1&token_auth=453170c79e8f0ad5dcd1f0b2ce1ecf23";
                    string url = "http://10.0.0.131:920/index.php?module=API&filter_limit=50&method=Live.getLastVisitsDetails&format=json&idSite=1&period=day&date=" + dt + "&expanded=1&token_auth=" + token;

                    if (maxVisitID > 0)
                        url = url + "&maxIdVisit=" + maxVisitID;

                    string xml = Boodoll.PageBL.ProductSearch.ProductSearchBLL.GetHtml(url, Encoding.GetEncoding("GB2312"));
                    xml = xml.Replace("\\u", "\\\\u");

                    Newtonsoft.Json.JavaScriptArray jsonObject = (Newtonsoft.Json.JavaScriptArray)Newtonsoft.Json.JavaScriptConvert.DeserializeObject(xml);

                    int count = jsonObject.Count();


                    for (int i = 0; i < count; i++)
                    {
                        JavaScriptObject qcount = (JavaScriptObject)jsonObject[i];
                        JavaScriptArray actionDetails = (JavaScriptArray)qcount["actionDetails"];

                        if (i == 0)
                            maxVisitID = Convert.ToInt64(qcount["idVisit"]);
                        else
                            maxVisitID = Convert.ToInt64(qcount["idVisit"]) > maxVisitID ? maxVisitID : Convert.ToInt64(qcount["idVisit"]);



                        if (actionDetails.Count > 0)
                        {
                            string userid = "";
                            string guid = "";
                            string referurl = "";

                            JavaScriptObject customVariables = null;
                            try
                            {
                                customVariables = (JavaScriptObject)qcount["customVariables"];
                                userid = ((new Dictionary<string, object>(((Newtonsoft.Json.JavaScriptObject)((new Dictionary<string, object>(customVariables)).ElementAt(0).Value)))).ElementAt(1).Value.ToString());
                                guid = (new Dictionary<string, object>(((Newtonsoft.Json.JavaScriptObject)((new Dictionary<string, object>(customVariables)).ElementAt(1).Value)))).ElementAt(1).Value.ToString();

                                actionDetails.ForEach(item =>
                               {
                                   JavaScriptObject itemobject = (JavaScriptObject)item;


                                   if (itemobject["type"].Equals("ecommerceAbandonedCart"))
                                   {

                                       // 



                                       Newtonsoft.Json.JavaScriptArray ItemDetails = (Newtonsoft.Json.JavaScriptArray)itemobject["itemDetails"];
                                       ItemDetails.ForEach(tmp_item =>
                                      {
                                          Console.WriteLine(((JavaScriptObject)(tmp_item))["price"]);
                                          Console.WriteLine(((JavaScriptObject)(tmp_item))["itemSKU"]);
                                          Console.WriteLine(((JavaScriptObject)(tmp_item))["quantity"]);
                                          //  +		[4]	{[quantity, 1]}	System.Collections.Generic.KeyValuePair<string,object>

                                          //		(new System.Collections.Generic.Mscorlib_DictionaryDebugView<string,object>(((Newtonsoft.Json.JavaScriptObject)((tmp_item))))).Items[0].Value	"33202"	object {string}

                                          // Console.WriteLine((Newtonsoft.Json.JavaScriptArray)tmp_item["sku"]);
                                      });
                                       Console.WriteLine(ItemDetails.Count);
                                       //  var q = customerDetail.Values.First();
                                       //string q1 = (new Dictionary<string, object>(((Newtonsoft.Json.JavaScriptObject)((new Dictionary<string, object>(ItemDetail)).ElementAt(0).Value)))).ElementAt(1).Value.ToString();
                                       //string q2 = (new Dictionary<string, object>(((Newtonsoft.Json.JavaScriptObject)((new Dictionary<string, object>(ItemDetail)).ElementAt(0).Value)))).ElementAt(0).Value.ToString();

                                       //     (new Dictionary<string, object>(customerDetail));
                                       //(new System.Collections.Generic.Mscorlib_DictionaryDebugView<string,object>(((Newtonsoft.Json.JavaScriptObject)((new System.Collections.Generic.Mscorlib_DictionaryDebugView<string,object>(customerDetail)).Items[0].Value)))).Items[1].Value  
                                       // (new System.Collections.Generic.Mscorlib_DictionaryDebugView<string,object>(((Newtonsoft.Json.JavaScriptObject)((new System.Collections.Generic.Mscorlib_DictionaryDebugView<string,object>(q)).Items[0].Value)))).Items[1].Value 


                                   }



                               });
                                Console.WriteLine(mi);
                            }
                            catch (Exception ex)
                            {
                                continue;
                            }


                        }

                    }

                    #endregion
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }



        }


        


        public static void writeLog(string writeFile, Dictionary<string,string> urls)
        {


            try
            {
                FileStream fs = new FileStream(writeFile, FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter wr = new StreamWriter(fs);

                foreach (KeyValuePair<string, string> ss in urls)
                {
                    if(ss.Key.Length>0&&ss.Value.Length>0)
                        wr.WriteLine(string.Format("update piwik_log_reffer set refergbk='{0}' where refferurl='{1}' ;", ss.Value, ss.Key));
                }


                wr.Flush();

                wr.Close();
                fs.Close();
            }
            catch
            { }
        }

    }
}
