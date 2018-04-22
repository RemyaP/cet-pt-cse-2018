using DataAccess.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstateManagement
{
    public class Utils
    {
        public static int GetSeller()
        {
            int sellerid = 0;
            Object user = System.Web.HttpContext.Current.Session["UserId"];
            if( null != user )
            {
                long userid = (long)user;
                using( RealEntities db = new RealEntities() )
                {
                    seller seller = db.sellers.Where( s => s.user_id == userid ).FirstOrDefault();
                    sellerid = seller.seller_id;
                }
            }
            return sellerid;
        }

        public static int GetBuyer()
        {
            int buyerid = 0;
            Object user = System.Web.HttpContext.Current.Session["UserId"];
            if( null != user )
            {
                long userid = (long)user;
                using( RealEntities db = new RealEntities() )
                {
                    buyer buyer = db.buyers.Where( b => b.user_id == userid ).FirstOrDefault();
                    buyerid = buyer.buyer_id;
                }
            }
            return buyerid;
        }

        public static long GetUser()
        {
            long userid = 0;
            Object user = System.Web.HttpContext.Current.Session["UserId"];
            if( null != user )
            {
                userid = ( long )user;
            }
            return userid;
        }
    }
}