//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess.EntityModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class user
    {
        public user()
        {
            this.buyers = new HashSet<buyer>();
            this.sellers = new HashSet<seller>();
        }
    
        public long user_id { get; set; }
        public string user_name { get; set; }
        public string password { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string address { get; set; }
        public string pan_no { get; set; }
        public string mob_no { get; set; }
        public string email { get; set; }
        public int status_id { get; set; }
    
        public virtual ICollection<buyer> buyers { get; set; }
        public virtual ICollection<seller> sellers { get; set; }
        public virtual status status { get; set; }
    }
}
