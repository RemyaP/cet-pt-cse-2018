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
    
    public partial class seller
    {
        public seller()
        {
            this.properties = new HashSet<property>();
        }
    
        public int seller_id { get; set; }
        public int seller_type { get; set; }
        public long user_id { get; set; }
    
        public virtual ICollection<property> properties { get; set; }
        public virtual user user { get; set; }
    }
}
