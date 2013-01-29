using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication2
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

        public static bool insert_piwiklog(List<UserVisitInfo> usrlist)
        {
            bool flag = false;

            List<Piwik_log> eventList = new List<Piwik_log>();

            try
            {
                foreach (UserVisitInfo e in usrlist)
                {
                    eventList.Add(new Piwik_log()
                    {
                         guid=e.Guid,
                         action=e.Action??"",
                          lastVisitTime=DateTime.Parse(e.LastVisitTime??"1990-1-1"),
                          spenttime=int.Parse(e.Spent??"0"),
                           pagetitle=e.PageTitle??"",
                            url=e.Url,
                             userid=int.Parse(e.Userid??"-1")



                    });

                }

                offlineBbhomeDataContext ctx = new offlineBbhomeDataContext();
                ctx.Piwik_logs.InsertAllOnSubmit(eventList);
                ctx.SubmitChanges();

                flag = true;

            }
            catch (Exception ex)
            {
                return false;
            }
            return flag;
        }






      


    }
}
