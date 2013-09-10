using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SyncUtil;

namespace SyncDataBll
{
    public class BasePiwikData
    {
        public static bool UpdatePiwikColumn(int type,int day)
        {
            using(OffBbhomeDataContext ctx=new OffBbhomeDataContext())
            {
                DateTime endTime=DateTime.Parse(DateTime.Now.ToShortDateString()).AddDays(day);
                List<Piwik_ColumnWord> pwkWords = new List<Piwik_ColumnWord>();
                if(ctx.Piwik_logs.Any(c=>c.lastVisitTime>endTime))
                {
                      switch(type)
                      {
                          case 1:
                                var sourceObj=ctx.Piwik_logs.Where(c=>c.lastVisitTime>endTime&&!string.IsNullOrEmpty(c.refferurl)).Select(c=>c.refferurl).Distinct();

                                var targetObj=ctx.Piwik_ColumnWords.Where(p => p.type == type && sourceObj.Contains(p.refferurl)).Select(c=>c.refferurl).Distinct();
                                if (targetObj.Count() < sourceObj.Count())
                                {
                                    var insertObj = sourceObj;
                                    if(targetObj.Count()>0)
                                     insertObj = sourceObj.Except(targetObj);
                                    foreach (string txt in insertObj)
                                    {
                                        pwkWords.Add(
                                            new Piwik_ColumnWord()
                                            {
                                                 refferurl=txt,
                                                  type=type,
                                                   refferurlUTF=SyncUtil.WebUtility.ConvertUTF8(txt)
                                            }

                                            );

                                    }


                                    if (pwkWords.Count > 0)
                                    {
                                        ctx.Piwik_ColumnWords.InsertAllOnSubmit(pwkWords);
                                        ctx.SubmitChanges();
                                    }
                                }
                                break;
                          default:

                              break;
                      }
                }

              
                return true;
            }
        }
    }
}
