namespace RealEstateManagement.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("real_estate.images")]
    public partial class image
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int property_id { get; set; }

        [Key]
        [Column("image", Order = 1, TypeName = "blob")]
        public byte[] image1 { get; set; }

        public virtual property property { get; set; }
    }
}
