namespace RealEstateManagement.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("real_estate.landmark")]
    public partial class landmark
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public landmark()
        {
            properties = new HashSet<property>();
        }

        [Key]
        [Column(TypeName = "uint")]
        public long landmark_id { get; set; }

        [StringLength(100)]
        public string name { get; set; }

        public double? latitude { get; set; }

        public double? longitude { get; set; }

        public int landmarktype { get; set; }

        public virtual landmarktype landmark_type { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<property> properties { get; set; }
    }
}
