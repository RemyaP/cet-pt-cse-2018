namespace RealEstateManagement.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("real_estate.bid")]
    public partial class bid
    {
        [Key]
        public int bid_id { get; set; }

        public int property_id { get; set; }

        public int buyer_id { get; set; }

        [Column(TypeName = "uint")]
        public long? status { get; set; }

        public virtual bid_price bid_price { get; set; }

        public virtual buyer buyer { get; set; }

        public virtual property property { get; set; }
    }
}
