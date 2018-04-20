namespace RealEstateManagement.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table( "real_estate.propery_status" )]
    public partial class property_status
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public property_status()
        {
            properties = new HashSet<property>();
        }

        [Key]
        public int status_id { get; set; }

        [Column("status")]
        [Required]
        [StringLength(45)]
        public string status1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<property> properties { get; set; }
    }
}
