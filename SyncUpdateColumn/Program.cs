using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SyncDataBll;
namespace SyncUpdateColumn
{
    class Program
    {
        public static string dfrom = System.Configuration.ConfigurationSettings.AppSettings["updateform"];
        static void Main(string[] args)
        {
           bool flag= BasePiwikData.UpdatePiwikColumn(1,int.Parse(dfrom));
           if (flag)
           {
               Console.WriteLine("utf-8转换成功");
           }
           else
           {
               Console.WriteLine("utf-8转换失败");
           }

           System.Threading.Thread.Sleep(120);
        }
    }
}
