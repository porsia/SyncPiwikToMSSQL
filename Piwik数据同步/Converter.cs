using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication2
{

    /// <summary>
    /// 数据转换类
    /// </summary>
    public class Converter
    {

        /// <summary>
        /// 获得int
        /// </summary>
        /// <param name="obj">转换前值</param>
        /// <param name="defValue">默认Int值</param>
        /// <returns>转换后值</returns>
        public static int ParseInt(object obj, int defValue)
        {
            int result = -1;
            try
            {
                string res = obj.ToString();
                if (!int.TryParse(res, out result))
                {
                    result = defValue;
                }
            }
            catch
            {
                result = defValue;
            }
            return result;
        }

        /// <summary>
        /// obj转string
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defValue"></param>
        /// <returns></returns>
        public static string ParseString(object obj, string defValue)
        {
            string result = string.Empty;
            try
            {
                if (obj == null)
                    result = defValue;
                else
                    result = obj.ToString();

            }
            catch
            {
                result = defValue;
            }
            return result;
        }
        /// <summary>
        /// 获得Decimal
        /// </summary>
        /// <param name="obj">转换前值</param>
        /// <param name="defValue">默认decimal值</param>
        /// <returns>转换后值</returns>
        public static decimal ParseDecimal(object obj, decimal defValue)
        {
            decimal result = new decimal(0);
            try
            {
                string res = obj.ToString();
                if (!decimal.TryParse(res, out result))
                {
                    result = defValue;
                }
            }
            catch
            {
            }
            return result;
        }

        /// <summary>
        /// 获得Datetime
        /// </summary>
        /// <param name="obj">转换前值</param>
        /// <param name="defValue">默认时间</param>
        /// <returns>转换后值</returns>
        public static DateTime ParseDateTime(object obj, DateTime defValue)
        {
            DateTime result = DateTime.Now;
            try
            {
                if (!DateTime.TryParse(obj.ToString(), out result))
                {
                    result = defValue;
                }
            }
            catch
            {

            }
            return result;
        }

        /// <summary>
        /// 获得布尔值
        /// </summary>
        /// <param name="obj">转换前的值</param>
        /// <param name="defValue">默认值</param>
        /// <returns>转换后的值</returns>
        public static bool ParseBool(object obj, bool defValue)
        {
            bool result = false;
            try
            {
                if (!bool.TryParse(obj.ToString(), out result))
                {
                    result = defValue;
                }
            }
            catch
            {

            }
            return result;
        }

        /// <summary>
        /// 转换IP
        /// </summary>
        /// <param name="ip">字符串IP值</param>
        /// <param name="defIP">默认值</param>
        /// <returns>转换后的值</returns>
        public static decimal SIP2IIP(string ip, decimal defIP)
        {
            decimal result = new decimal(0);

            if (!string.IsNullOrEmpty(ip) && ip.IndexOf('.') > 0)
            {
                string[] ips = ip.Split('.');
                if (ips.Length < 4)
                {
                    for (int i = 0; i < 4 - ips.Length; i++)
                    {
                        ip += ".0";
                    }
                    ips = ip.Split('.');
                }

                for (int i = 0; i < ips.Length; i++)
                {
                    result = result * 256 + Converter.ParseDecimal(ips[i], new decimal(0)) * 256;
                }
            }

            return result;
        }

       

        public static int ParseInt(object p)
        {
            throw new NotImplementedException();
        }
    }
}
