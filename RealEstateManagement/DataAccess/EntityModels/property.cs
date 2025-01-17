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
    
    public partial class property
    {
        public property()
        {
            this.bids = new HashSet<bid>();
            this.images = new HashSet<image>();
            this.landmarks = new HashSet<landmark>();
        }
    
        public int property_id { get; set; }
        public string name { get; set; }
        public Nullable<double> latitude { get; set; }
        public Nullable<double> longitude { get; set; }
        public Nullable<double> area { get; set; }
        public int seller_id { get; set; }
        public int status { get; set; }
        public int category { get; set; }
    
        public virtual ICollection<bid> bids { get; set; }
        public virtual category category1 { get; set; }
        public virtual min_price min_price { get; set; }
        public virtual ICollection<image> images { get; set; }
        public virtual seller seller { get; set; }
        public virtual propery_status propery_status { get; set; }
        public virtual ICollection<landmark> landmarks { get; set; }
    }
}
