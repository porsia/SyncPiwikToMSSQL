using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication2
{
    public class UserVisitInfo
    {
        string userid;
        string url;
        int type;
        string lastVisitTime;
        string guid;
        string mobile;
        string pageTitle;
        string action;
        string spent;

        public string Spent
        {
            get { return spent; }
            set { spent = value; }
        }

        public string Action
        {
            get { return action; }
            set { action = value; }
        }

        public string PageTitle
        {
            get { return pageTitle; }
            set { pageTitle = value; }
        }

        public string Mobile
        {
            get { return mobile; }
            set { mobile = value; }
        }

        public string Userid
        {
            get { return userid; }
            set { userid = value; }
        }


        public string Guid
        {
            get { return guid; }
            set { guid = value; }
        }


        public string Url
        {
            get { return url; }
            set { url = value; }
        }



        public string LastVisitTime
        {
            get { return lastVisitTime; }
            set { lastVisitTime = value; }
        }



        public int Type
        {
            get { return type; }
            set { type = value; }
        }
    }
}
