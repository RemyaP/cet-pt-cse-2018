using DataAccess.EntityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RealEstateManagement.Models
{
    public enum UserType { Buyer, Seller, Both}
    public class UserViewModel
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PAN { get; set; }
        public string Mob { get; set; }
        public string Email { get; set; }
        public int Status { get; set; }
        public UserType Type { get; set; }

        public bool RegisterUser()
        {
            try
            {
                RealEntities db = new RealEntities();
                user u = new user();
                u.user_name = UserName;
                u.password = Password;
                u.first_name = FirstName;
                u.last_name = LastName;
                u.address = Address;
                u.pan_no = PAN;
                u.mob_no = Mob;
                u.email = Email;
                u.status_id = 1;
                db.users.Add( u );
                db.SaveChanges();
                UserId = u.user_id;
                switch ( Type )
                {
                    case UserType.Buyer:
                        {
                            buyer b = new buyer();
                            b.user_id = u.user_id;
                            db.buyers.Add( b );
                            break;
                        }
                    case UserType.Both:
                        {
                            buyer b = new buyer();
                            b.user_id = u.user_id;
                            db.buyers.Add( b );
                            seller s = new seller();
                            s.seller_type = 2;
                            s.user_id = u.user_id;
                            db.sellers.Add( s );
                            break;
                        }
                    case UserType.Seller:
                        {
                            seller s = new seller();
                            s.seller_type = 1;
                            s.user_id = u.user_id;
                            db.sellers.Add( s );
                            break;
                        }
                }
                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
            }
            return false;
        }

        public bool UserLogin()
        {
            try
            {
                RealEntities db = new RealEntities();
                user user = db.users.Where(u => u.user_name == UserName && u.password == Password).FirstOrDefault();
                if( null == user ) return false;
                UserId = user.user_id;
                seller seller = db.sellers.Where(s => s.user_id == user.user_id).FirstOrDefault();
                buyer buyer = db.buyers.Where(b => b.user_id == user.user_id).FirstOrDefault();

                if( null != buyer && null != seller ) Type = UserType.Both;
                else if( null != buyer ) Type = UserType.Buyer;
                else if( null != seller ) Type = UserType.Seller;
                return true;
            }
            catch( Exception ex )
            {
            }
            return false;
        }
    }
}