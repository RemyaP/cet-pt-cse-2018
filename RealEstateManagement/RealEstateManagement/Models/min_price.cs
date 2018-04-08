namespace RealEstateManagement.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("real_estate.min_price")]
    public partial class min_price
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int property_id { get; set; }

        public double? plot_price { get; set; }

        public double? apartment_price { get; set; }

        public virtual property property { get; set; }
    }
}
