using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

using System.Collections;
using System.Collections.Specialized;

namespace SyncPiwikToMSSQL
{
    public class DataFarm
    {
        public static bool ExistGaData(int type, DateTime st, DateTime et)
        {
            bool flag = false;
            try
            {

                offlineBbhomeDataContext ctx = new offlineBbhomeDataContext();
                flag = ctx.GA_Logs.Any(c => c.Type == type && c.GAStartDate == st && c.GAEndDate == et);

                if (!flag)
                {
                    InserGaBaseData(type, st, et, false);
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return flag;
        }
        public static bool InserGaBaseData(int type, DateTime st, DateTime et, bool f)
        {
            bool flag = false;
            try
            {
                offlineBbhomeDataContext ctx = new offlineBbhomeDataContext();
                ctx.GA_Logs.InsertOnSubmit(
                    new GA_Log()
                    {
                        Type = type,
                        GAStartDate = st,
                        GAEndDate = et,
                        Status = flag
                    }
                    );
                ctx.SubmitChanges();
                flag = true;
            }
            catch (Exception ex)
            {
                return false;
            }

            return flag;
        }


        public static bool UpdateGaBaseData(int type, DateTime st, DateTime et)
        {
            bool flag = false;
            try
            {
                offlineBbhomeDataContext ctx = new offlineBbhomeDataContext();
                flag = ctx.GA_Logs.Any(c => c.Type == type && c.GAStartDate == st && c.GAEndDate == et && c.Status == false);

                if (flag)
                {
                    flag = false;
                    GA_Log log = null;
                    log = ctx.GA_Logs.FirstOrDefault(c => c.Type == type && c.GAStartDate == st && c.GAEndDate == et && c.Status == false);

                    if (log != null)
                    {
                        log.Status = true;

                        ctx.SubmitChanges();
                        flag = true;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return flag;
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

                        newString = newString.Replace(key + "=" + value, key + "=" + MyUrlDeCode(value, encoding));
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

        public static bool insert_piwik_log_reffer(List<string> refferList)
        {
                using (offlineBbhomeDataContext ctx = new offlineBbhomeDataContext())
                {
                    try
                    {
                        var query = refferList.Where(c => !ctx.piwik_log_reffers.Any(p => p.refferurl == c)).ToList();

                        List<piwik_log_reffer> addreffers = new List<piwik_log_reffer>();
                        query.ForEach(
                        c =>
                        {
                            string tstr = c.ToString();
                            string durl = tstr;
                            if (!string.IsNullOrEmpty(tstr) && tstr != "http://about:blank")
                                durl = GetNewString(tstr, null, true);

                
                      
                            addreffers.Add(
                                new piwik_log_reffer()
                                {
                                    refferurl = c.ToString(),
                                    refergbk=durl
                                }
                                )

                                ;
                        });

                        ctx.piwik_log_reffers.InsertAllOnSubmit(addreffers);
                        ctx.SubmitChanges();
                    }
                    catch
                    {
                        return false;
                    }

                    return true;

            }
                    
               
        }

        public static bool insert_piwiklog(List<UserVisitInfo> usrlist,out string msg)
        {
            bool flag = false;
            msg = "";
            try
            {
                 List<Piwik_log> eventList = new List<Piwik_log>();
                 foreach (UserVisitInfo e in usrlist)
                 {
       
                        eventList.Add(new Piwik_log()
                        {
                             guid=e.Guid,
                             action=e.Action??"",
                              lastVisitTime=DateTime.Parse(e.LastVisitTime??"1990-1-1"),
                              spenttime=Converter.ParseInt(e.Spent,0),
                               pagetitle=e.PageTitle??"",
                                url=e.Url,
                                 userid=Converter.ParseInt(e.Userid,-1),
                                 refferurl=e.Referurl,
                                  event_action=e.Event_action,
                                   visitIp=e.VisitIp,
                                    location=e.Location,
                                    locationsina=e.Locationsina
                        });                  
                 }

                 offlineBbhomeDataContext ctx = new offlineBbhomeDataContext();
                 ctx.Piwik_logs.InsertAllOnSubmit(eventList);
                 ctx.SubmitChanges();

                 flag = true;

            }
            catch (Exception ex)
            {
                msg = ex.ToString();
                return false;
            }
      
            return flag;
        }


        public static bool insert_piwikCsAction(List<Piwik_CustomerAction> usrlist, out string msg)
        {
            bool flag = false;
            msg = "";
            try
            {
              

                offlineBbhomeDataContext ctx = new offlineBbhomeDataContext();
                ctx.Piwik_CustomerActions.InsertAllOnSubmit(usrlist);
                ctx.SubmitChanges();

                flag = true;

            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }

            return flag;
        }






      


    }
}
