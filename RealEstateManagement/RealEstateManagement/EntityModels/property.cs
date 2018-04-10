namespace RealEstateManagement.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("real_estate.property")]
    public partial class property
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public property()
        {
            bids = new HashSet<bid>();
            images = new HashSet<image>();
            landmarks = new HashSet<landmark>();
        }

        [Key]
        public int property_id { get; set; }

        public double? area { get; set; }

        public double? latitude { get; set; }

        public double? longitude { get; set; }

        public int seller_id { get; set; }

        public int category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<bid> bids { get; set; }

        public virtual category category1 { get; set; }

        public virtual min_price min_price { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<image> images { get; set; }

        public virtual seller seller { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<landmark> landmarks { get; set; }
    }
}
