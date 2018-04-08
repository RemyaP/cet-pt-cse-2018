namespace RealEstateManagement.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("real_estate.bid_price")]
    public partial class bid_price
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int bid_id { get; set; }

        public double? plot_price { get; set; }

        public double? apartment_price { get; set; }

        public virtual bid bid { get; set; }
    }
}
