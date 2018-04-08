namespace RealEstateManagement.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("real_estate.buyer")]
    public partial class buyer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public buyer()
        {
            bids = new HashSet<bid>();
        }

        [Key]
        public int buyer_id { get; set; }

        [Column(TypeName = "uint")]
        public long? min_cost { get; set; }

        [Column(TypeName = "uint")]
        public long? max_cost { get; set; }

        [Column(TypeName = "uint")]
        public long user_id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<bid> bids { get; set; }

        public virtual user user { get; set; }
    }
}
