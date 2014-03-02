using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SyncUtil
{
    public   class WebUtility
    {
        public static string ConvertUTF8(string word)
        {

            //word是UrlEncode编码字符串 
            Encoding gb2312 = Encoding.GetEncoding("gb2312");
            Encoding utf8 = Encoding.UTF8;
            //首先用utf-8进行解码 
            string key = HttpUtility.UrlDecode(word, utf8);
            // 将已经解码的字符再次进行编码. 
            string encode = HttpUtility.UrlEncode(key, utf8).ToLower();
            //与原来编码进行对比，如果不一致说明解码未正确，用gb2312进行解码 
            if (word != encode)
                key = HttpUtility.UrlDecode(word, gb2312);

            return key;
            
        }
    }
}
