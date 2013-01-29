using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Web;
using System.Net;



using Boodoll.PageBL;
using Newtonsoft.Json;

namespace ConsoleApplication2
{
 

    class Program
    {

       // public static  Dictionary<int, string> allProductName = getAllProductIDName();
        public static string dfrom = System.Configuration.ConfigurationSettings.AppSettings["updateform"];
        public static string token = System.Configuration.ConfigurationSettings.AppSettings["token"];
        static void Main(string[] args)
        {
      

            Console.WriteLine("开始分析数据:");


             int pageType = 31;
             for (int d = int.Parse(dfrom); d < 0; d++)
             {
                 string writeFile = string.Format("{0}\\result{1}.txt", System.Threading.Thread.GetDomain().BaseDirectory, DateTime.Now.ToFileTimeUtc().ToString());
           
                 List<UserVisitInfo> userList = new List<UserVisitInfo>();
         
                 string dt = DateTime.Now.AddDays(d).ToString("yyyy-MM-dd");


                 if (!DataFarm.ExistGaData(pageType, DateTime.Parse(dt), DateTime.Parse(dt)))
                 {
                     //数据读取完毕退出
                     bool exitFlag = false;
                     Int64 maxVisitID = 0;

                     try
                     {

                         for (int mi = 0; mi < int.MaxValue; mi++)
                         {
                             if (exitFlag)
                                 break;

                             #region 循环读取数据

                             // string url = "http://click.muyingzhijia.com/index.php?module=API&filter_limit=100&method=Live.getLastVisitsDetails&format=json&idSite=1&period=day&date=" + dtStr + "&expanded=1&token_auth=453170c79e8f0ad5dcd1f0b2ce1ecf23";
                             string url = "http://click.muyingzhijia.com/index.php?module=API&filter_limit=50&method=Live.getLastVisitsDetails&format=json&idSite=1&period=day&date=" + dt + "&expanded=1&token_auth=" + token;
                             ;

                             if (maxVisitID > 0)
                                 url = url + "&maxIdVisit=" + maxVisitID;

                             string xml = Boodoll.PageBL.ProductSearch.ProductSearchBLL.GetHtml(url, Encoding.GetEncoding("GB2312"));

                             Newtonsoft.Json.JavaScriptArray jsonObject = (Newtonsoft.Json.JavaScriptArray)Newtonsoft.Json.JavaScriptConvert.DeserializeObject(xml);

                             int count = jsonObject.Count();
 

                             for (int i = 0; i < count; i++)
                             {

                                 JavaScriptObject qcount = (JavaScriptObject)jsonObject[i];


                                 // qcount["actionDetails"];
                                 JavaScriptArray actionDetails = (JavaScriptArray)qcount["actionDetails"];


                                 string lastActionDateTime = qcount["serverDate"].ToString() + " " + qcount["serverTimePretty"].ToString();
                                 if (Convert.ToDateTime(lastActionDateTime) < Convert.ToDateTime(dt))
                                 {
                                     exitFlag = true;
                                     break;

                                 }
                                 //if(userList.Count>5)
                                 //{
                                 //    exitFlag = true;
                                 //    break;

                                 //}

                                 //if (Convert.ToDateTime(lastActionDateTime) < DateTime.Now.AddHours(-1))
                                 //{
                                 //    exitFlag = true;
                                 //    break;

                                 //}

                                 if (i == 0)
                                     maxVisitID = Convert.ToInt64(qcount["idVisit"]);
                                 else
                                     maxVisitID = Convert.ToInt64(qcount["idVisit"]) > maxVisitID ? maxVisitID : Convert.ToInt64(qcount["idVisit"]);

                                 if (actionDetails.Count > 0)
                                 {
                                     string userid = "";
                                     string guid = "";

                                     JavaScriptObject customVariables = null;
                                     try
                                     {
                                         customVariables = (JavaScriptObject)qcount["customVariables"];
                                         userid = ((new Dictionary<string, object>(((Newtonsoft.Json.JavaScriptObject)((new Dictionary<string, object>(customVariables)).ElementAt(0).Value)))).ElementAt(1).Value.ToString());
                                         guid = (new Dictionary<string, object>(((Newtonsoft.Json.JavaScriptObject)((new Dictionary<string, object>(customVariables)).ElementAt(1).Value)))).ElementAt(1).Value.ToString();
                                         if (guid == "msyfrf55bsvfd1uloppznp4520130127113133121")
                                         {
                                             string test = "test";
                                         }

                                     }
                                     catch (Exception ex)
                                     {
                                         continue;
                                     }




                                     //message = getUserInfo(guid, userid);


                                     actionDetails.ForEach(item =>
                                     {
                                         JavaScriptObject itemobject = (JavaScriptObject)item;


                                         if (itemobject.Keys.Contains("url") && itemobject["url"] != null)
                                         {
                                             string tmpUrl = itemobject["url"].ToString();

                                             UserVisitInfo vinfo = new UserVisitInfo();
                                             if (vinfo != null)
                                             {
                                                 vinfo.Guid = guid;
                                                 vinfo.Userid = userid;
                                                 if (itemobject.Keys.Contains("timeSpent"))
                                                     vinfo.Spent = Converter.ParseString(itemobject["timeSpent"], "");

                                                 if (itemobject.Keys.Contains("url"))
                                                     vinfo.Url = GetProductID(Converter.ParseString(itemobject["url"], ""));
                                                 vinfo.LastVisitTime = lastActionDateTime;

                                                 if (itemobject.Keys.Contains("pageTitle"))
                                                     vinfo.PageTitle = Converter.ParseString(itemobject["pageTitle"], "");// ConvertUnicodeStringToChinese(Converter.ParseString(itemobject["pageTitle"], ""));
                                                 if (itemobject.Keys.Contains("type"))
                                                     vinfo.Action = Converter.ParseString(itemobject["type"], "");

                                                 userList.Add(vinfo);

                                                 //List<UserVisitInfo> vt = new List<UserVisitInfo>();
                                                 //vt.Add(vinfo);
                                                 //DataFarm.insert_piwiklog(vt);

                                             }


                                         }
                                     });
                                 }


                                 Console.WriteLine(DateTime.Now + "," + dt + "," + i + "," + mi+ "," + "finish.");

                             }




                             #endregion
                         }
                         }
                          catch (Exception ex)
                         {
                             Console.WriteLine(ex.Message);
                         }
                    
                    
                     userList = userList.Distinct().ToList();
                     if (userList.Count > 0)
                     {
                         if (DataFarm.insert_piwiklog(userList))
                         {
                             DataFarm.UpdateGaBaseData(pageType, DateTime.Parse(dt), DateTime.Parse(dt));
                             Console.WriteLine(DateTime.Now + "," + pageType + "," + dt + "," + "finish.");
                         }
                     }
                 
                        
                    
                    // CreateReport(userList);
                     writeLog(writeFile, userList);
                 
                 }
             }

            Console.WriteLine("数据生成完毕");
            Console.ReadLine();
          
        }
        public static string ConvertUnicodeStringToChinese(string unicodeString)
        {
            if (string.IsNullOrEmpty(unicodeString))
                return string.Empty;

            string outStr = unicodeString;

            Regex re = new Regex("[0123456789abcdef]{4}", RegexOptions.IgnoreCase);
            MatchCollection mc = re.Matches(unicodeString);
            foreach (Match ma in mc)
            {
                outStr = outStr.Replace(ma.Value, ConverUnicodeStringToChar(ma.Value).ToString());
            }
            return outStr;
        }


        private static char ConverUnicodeStringToChar(string str)
        {
            char outStr = Char.MinValue;
            outStr = (char)int.Parse(str, System.Globalization.NumberStyles.HexNumber);
            return outStr;
        }

        public static string unicode2str(string str)
        {

            string outStr = "";
            if (!string.IsNullOrEmpty(str))
            {
             //   string[] strlist = str.Replace("\\", "").Split('u');
                int len = str.Length / 4;
                try
                {
                    for (int i = 0; i < len; i++)
                    {
                        string src = str.Substring(i*4,4);
                        //将unicode字符转为10进制整数，然后转为char中文字符
                        outStr += (char)int.Parse(src, System.Globalization.NumberStyles.HexNumber);
                    }
                }
                catch (FormatException ex)
                {
                    outStr = ex.Message;
                }
            }

            return outStr;
        }


        public static string GetProductID(string url)
        {
            
            url = url.ToLower();

     
                string Pattern = "pdtid=[0-9]*"; // @"pdtID";
                MatchCollection Matches = Regex.Matches(url, Pattern, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);

                if (Matches.Count > 0)
                {
                    int i = 0;
                    string[] sUrlList = new string[Matches.Count];

                    // 取得匹配项列表
                    foreach (Match match in Matches)
                    {
                        sUrlList[i++] = match.Value.Replace("pdtid=", "");

                    }


                    return string.Join(",", sUrlList);
                }

                if (url.Length > 2000)
                    url = url.Substring(0,2000);
                
                return url;

        }

   

        public static UserVisitInfo CreateUserInfo(string url,int type,string userid,string guid)
        {
            List<string> prds = GetVisitProductByType(type);

            url = url.ToLower();

            //if (type == 6 && url.Contains("23101"))
            //{
            //    bool aa = false;
            //}

            string productInfo = "";
            if(prds.Any(p => url.ToString().Contains(p)))
            {
                string Pattern = "pdtid=[0-9]*"; // @"pdtID";
                MatchCollection Matches = Regex.Matches(url, Pattern, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);

                int i = 0;
                string[] sUrlList = new string[Matches.Count];

                // 取得匹配项列表
                foreach (Match match in Matches)
                {
                    string prd = "";// allProductName[Convert.ToInt32(match.Value.Replace("pdtid=", ""))];
                    sUrlList[i++] = match.Value.Replace("pdtid", prd);

                }


                productInfo=string.Join(",", sUrlList);
                Console.WriteLine(productInfo + "," + userid + "," + guid);


                return new UserVisitInfo() { Type = type, Userid = userid, Guid = guid, Url = productInfo };

            }

            return null;
             
           
        }


        public static int getUserInfo(string guid, string usrid)
        {
            string memssage = "";
            int uid =Converter.ParseInt(usrid, -1);

            if (uid < 0)
            {
                offlineBbhomeDataContext octx = new offlineBbhomeDataContext();
                if (octx.Ga_guidUserIDs.Any(c => c.guid == guid))
                {
                    uid = octx.Ga_guidUserIDs.FirstOrDefault(c => c.guid == guid).uid;
                }
            }

       
              
            return uid;
        }


        public static void CreateReport(List<UserVisitInfo> u)
        {
            string body="<html><body><H3>4小时内数据报表click.muyingzhijia.com</H3>";
            for (int i = 1; i < 12; i++)
            {
                string head = " <H3>"+getIDName(i)+"</H3><table border = 1>   <tr>     <th> 会员号 </th> <th>手机号  </th><th> 浏览商品 </th>  <th> 浏览时间</th></tr>";
                foreach (UserVisitInfo a in u.Where(c=>c.Type==i))
                {
                    head += ("<tr><td>" + a.Userid + "</td><td>" + a.Mobile + "</td><td>" + a.Url + "</td><td>" + a.LastVisitTime + "</td><td></tr>");//开始写入值

                }

                head += "</table>";
                body += head;
            }
            body+="</body></html>";




            EmailServiceClient esc = new  EmailServiceClient();
                       esc.Open();
                       esc.SendCmail(new WCFService.WcfMail() { Body = body, Subject = "4小时内数据报表click.muyingzhijia.com", MailTo = ("wm1240@muyingzhijia.com; ws632@muyingzhijia.com; sd211@muyingzhijia.com;porsia@muyingzhijia.com;yxd1279@muyingzhijia.com;wh971@muyingzhijia.com;lyq942@muyingzhijia.com; cfzmp@163.com".Split(new char[] { ',', ';' })), IsHtml = true });
            esc.Close();
        }



        public static void writeLog(string writeFile,List<UserVisitInfo> u)
        {
          
      
            try
            {
                FileStream fs = new FileStream(writeFile, FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter wr = new StreamWriter(fs);

                foreach (UserVisitInfo a in u)
                {
                    wr.WriteLine(a.Userid + "," + a.Mobile + "," + a.Url + "," + a.Type + "," + a.LastVisitTime);//开始写入值

                }

                wr.Flush();

                wr.Close();
                fs.Close();
            }
            catch
            { }
        }

        public static Dictionary<int, string> getAllProductIDName()
        {
               var q=(
                    from c in new HolycaDataContext().Vi_Web_Pdt_Lists
                    select new { c.intProductID, c.vchProductName }
                ).Distinct().ToList();
               Dictionary<int, string> dics = new Dictionary<int, string>();

               q.ForEach(c =>
                   {
                       dics.Add( c.intProductID,c.vchProductName);
                   }
                   );
               return dics;
             
        }

        public static string getIDName(int type)
        {
    
            switch (type)
            {
                ///童床 10 and cateId2=64
                case 1:
                    return "童床";
                    break;
                // 童车
                case 2:
                    return "童车"; break;

                // 汽车座椅
                case 3:
                    return "汽车座椅"; break;
                //床品
                case 4:
                    return "床品"; break;

                //值300元以上的玩具
                case 5:
                    return "值300元以上的玩具"; break;
                //吸奶器  cateId1=6 and cateId2=40 and (productName like '%吸奶器%' or productName like '%吸乳器%'
                case 6:
                    return "吸奶器"; break;

                //消毒锅 cateId1=6 and cateId2=41 and (productName like '%消毒锅%' or productName like '%消毒器%')
                case 7:
                    return "消毒锅"; break;

                //LG地垫cateId1=11 and cateId2=70 and brandid=443
                case 8:
                    return "LG地垫"; break;

                //法贝儿 brandid=598
                case 9:
                    return "法贝儿"; break;

                //施巴 brandid=514 
                case 10:
                    return "施巴"; break;

                //和光堂 brandid=319 
                case 11:
                    return "和光堂"; break;

                default:

                    break;


            }


            return "";
        }



        public static List<string> GetVisitProductByType(int type)
        {
            HolycaDataContext ctx=new HolycaDataContext();


            List<string> tmpProducts = new List<string>();
            
            switch(type)
            {
                ///童床 10 and cateId2=64
                case 1:
                 tmpProducts=ctx.Vi_Web_Pdt_Lists.Where(c => c.intFirstCategory == 10 && c.intSecondCategory == 64).Select(c => string.Format("pdtid={0}", c.intProductID)).Distinct().ToList();
                 break;
                    // 童车
                case 2:
                 tmpProducts = ctx.Vi_Web_Pdt_Lists.Where(c => c.intFirstCategory == 10 && c.intSecondCategory == 62).Select(c => string.Format("pdtid={0}", c.intProductID)).Distinct().ToList();
                 break;

                // 汽车座椅
                case 3:
                 tmpProducts = ctx.Vi_Web_Pdt_Lists.Where(c => c.intFirstCategory == 10 && c.intSecondCategory == 63).Select(c => string.Format("pdtid={0}", c.intProductID)).Distinct().ToList();
                 break;

                //床品
                case 4:
                 tmpProducts = ctx.Vi_Web_Pdt_Lists.Where(c => c.intFirstCategory == 2 && c.intSecondCategory == 25).Select(c => string.Format("pdtid={0}", c.intProductID)).Distinct().ToList();
                 break;
                
                //值300元以上的玩具
                case 5:
                 tmpProducts = ctx.Vi_Web_Pdt_Lists.Where(c => c.intFirstCategory == 11 && c.intScore > 300).Select(c => string.Format("pdtid={0}", c.intProductID)).Distinct().ToList();
                 break;
                //吸奶器  cateId1=6 and cateId2=40 and (productName like '%吸奶器%' or productName like '%吸乳器%'
                case 6:
                 tmpProducts = ctx.Vi_Web_Pdt_Lists.Where(c => c.intFirstCategory == 6 && c.intSecondCategory == 40 && (c.vchProductName.Contains("吸奶器") || c.vchProductName.Contains("吸乳器"))).Select(c => string.Format("pdtid={0}", c.intProductID)).Distinct().ToList();
                 break;

                //消毒锅 cateId1=6 and cateId2=41 and (productName like '%消毒锅%' or productName like '%消毒器%')
                case 7:
                 tmpProducts = ctx.Vi_Web_Pdt_Lists.Where(c => c.intFirstCategory == 6 && c.intSecondCategory == 41 && (c.vchProductName.Contains("消毒锅") || c.vchProductName.Contains("消毒器"))).Select(c => string.Format("pdtid={0}", c.intProductID)).Distinct().ToList();
                 break;

                //LG地垫cateId1=11 and cateId2=70 and brandid=443
                case 8:
                 tmpProducts = ctx.Vi_Web_Pdt_Lists.Where(c => c.intFirstCategory == 11 && c.intSecondCategory == 70 && c.intBrandID == 443).Select(c => string.Format("pdtid={0}", c.intProductID)).Distinct().ToList();
                 break;

                //法贝儿 brandid=598
                case 9:
                 tmpProducts = ctx.Vi_Web_Pdt_Lists.Where(c => c.intBrandID == 598).Select(c => string.Format("pdtid={0}", c.intProductID)).Distinct().ToList();
                 break;

                //施巴 brandid=514 
                case 10:
                 tmpProducts = ctx.Vi_Web_Pdt_Lists.Where(c => c.intBrandID == 514).Select(c => string.Format("pdtid={0}", c.intProductID)).Distinct().ToList();
                 break;

                //和光堂 brandid=319 
                case 11:
                 tmpProducts = ctx.Vi_Web_Pdt_Lists.Where(c => c.intBrandID == 319).Select(c => string.Format("pdtid={0}", c.intProductID)).Distinct().ToList();
                 break;

                default:

                 break;
           

             }


            return tmpProducts;
        }

        public static string GetProductCode(int productid)
        {
            
            string productCode = "";
            HolycaDataContext ctx = new HolycaDataContext();
            if (ctx.Pdt_Base_Infos.Any(p => p.intProductID == productid))
            {
                productCode = ctx.Pdt_Base_Infos.FirstOrDefault(p => p.intProductID == productid).vchproductcode;

            }

            return productCode;
        }

    }
}
