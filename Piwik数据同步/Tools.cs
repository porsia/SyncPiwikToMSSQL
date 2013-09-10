using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Data;
using System.Text.RegularExpressions;
using System.Security.Cryptography;


namespace SyncPiwikToMSSQL
{
    public class Tools
    {
        #region IsUTF8
        /// <summary>
        /// IsUTF8
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsUTF8(string s)
        {
            byte[] buf = UrlToBytes(s);
            return IsUTF8(buf);
        }

        /// <summary>
        /// IsUTF8
        /// </summary>
        /// <param name="buf"></param>
        /// <returns></returns>
        public static bool IsUTF8(byte[] buf)
        {
            int i;
            byte cOctets; // octets to go in this UTF-8 encoded character  
            bool bAllAscii = true;
            long iLen = buf.Length;
            cOctets = 0;
            for (i = 0; i < iLen; i++)
            {
                if ((buf[i] & 0x80) != 0) bAllAscii = false;

                if (cOctets == 0)
                {
                    if (buf[i] >= 0x80)
                    {
                        do
                        {
                            buf[i] <<= 1;
                            cOctets++;
                        }
                        while ((buf[i] & 0x80) != 0);

                        cOctets--;
                        if (cOctets == 0)
                            return false;
                    }
                }
                else
                {
                    if ((buf[i] & 0xC0) != 0x80)
                        return false;
                    cOctets--;
                }
            }
            if (cOctets > 0)
                return false;
            if (bAllAscii)
                return false;
            return true;
        }
        #endregion

        #region UrlToBytes
        /// <summary>
        /// UrlToBytes
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static byte[] UrlToBytes(string url)
        {
            StringBuilder sb = new StringBuilder();
            int i = url.IndexOf('%');
            while (i >= 0)
            {
                if (url.Length < i + 3)
                {
                    break;
                }
                sb.Append(url.Substring(i, 3));
                url = url.Substring(i + 3);
                i = url.IndexOf('%');
            }
            string urlCoding = sb.ToString();
            if (string.IsNullOrEmpty(urlCoding))
                return new byte[0];

            urlCoding = urlCoding.Replace("%", string.Empty);
            int len = urlCoding.Length / 2;
            byte[] result = new byte[len];
            len *= 2;
            for (int index = 0; index < len; index++)
            {
                string s = urlCoding.Substring(index, 2);
                int b = int.Parse(s, System.Globalization.NumberStyles.HexNumber);
                result[index / 2] = (byte)b;
                index++;
            }
            return result;
        }
        #endregion

        #region url地址是否采用UTF-8编码
        /// <summary>
        /// url地址是否采用UTF-8编码
        /// </summary>
        public static bool IsUTF8Url
        {
            get
            {
                string url = string.Empty;
                if (System.Web.HttpContext.Current.Request.ServerVariables != null && System.Web.HttpContext.Current.Request.ServerVariables["QUERY_STRING"] != null)
                {
                    url = System.Web.HttpContext.Current.Request.ServerVariables["QUERY_STRING"];
                }
                //foreach(string key in System.Web.HttpContext.Current.Request.ServerVariables)
                //{
                //    System.Web.HttpContext.Current.Response.Write(key + " = " + System.Web.HttpContext.Current.Request.ServerVariables[key] + "<br>");
                //}
                return IsUTF8(url);
            }
        }
        #endregion

        #region RequestQueryString
        /// <summary>
        /// RequestQueryString
        /// </summary>
        /// <param name="queryString"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string RequestQueryString(string queryString, string name)
        {
            if (string.IsNullOrEmpty(queryString))
            {
                return string.Empty;
            }
            System.Collections.Specialized.NameValueCollection nv = null;
            if (IsUTF8Url)
            {
                nv = System.Web.HttpUtility.ParseQueryString(queryString, Encoding.UTF8);
            }
            else
            {
                nv = System.Web.HttpUtility.ParseQueryString(queryString, Encoding.GetEncoding("GB2312"));
            }
            if (nv == null || nv.Count == 0)
            {
                return string.Empty;
            }
            if (nv[name] != null)
            {
                return nv[name].Trim();
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// RequestQueryString
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string RequestQueryString(string name)
        {
            string queryString = string.Empty;
            if (System.Web.HttpContext.Current.Request.ServerVariables != null && System.Web.HttpContext.Current.Request.ServerVariables["QUERY_STRING"] != null)
            {
                queryString = System.Web.HttpContext.Current.Request.ServerVariables["QUERY_STRING"];
            }
            return RequestQueryString(queryString, name);
        }
        #endregion

    }//ClassEnd
}
