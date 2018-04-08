namespace RealEstateManagement.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("real_estate.user")]
    public partial class user
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public user()
        {
            buyers = new HashSet<buyer>();
            sellers = new HashSet<seller>();
        }

        [Key]
        [Column(TypeName = "uint")]
        public long user_id { get; set; }

        [Required]
        [StringLength(20)]
        public string user_name { get; set; }

        [Required]
        [StringLength(20)]
        public string password { get; set; }

        [StringLength(45)]
        public string first_name { get; set; }

        [StringLength(45)]
        public string last_name { get; set; }

        [StringLength(45)]
        public string address { get; set; }

        [StringLength(45)]
        public string pan_no { get; set; }

        [StringLength(45)]
        public string mob_no { get; set; }

        [StringLength(45)]
        public string email { get; set; }

        public int status_id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<buyer> buyers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<seller> sellers { get; set; }

        public virtual status status { get; set; }
    }
}
