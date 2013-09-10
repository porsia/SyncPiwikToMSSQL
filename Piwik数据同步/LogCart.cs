using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SyncPiwikToMSSQL
{
   public  class LogCart
    {
       private string guid;
       private int qty;
       private decimal price;
       private int productid;
       private DateTime lastVisitTime;
   

       public string Guid
       {
           get { return guid; }
           set { guid = value; }
       }
    

       public int Qty
       {
           get { return qty; }
           set { qty = value; }
       }
 

       public decimal Price
       {
           get { return price; }
           set { price = value; }
       }
  

       public int Productid
       {
           get { return productid; }
           set { productid = value; }
       }
     

       public DateTime LastVisitTime
       {
           get { return lastVisitTime; }
           set { lastVisitTime = value; }
       }
   }
}
