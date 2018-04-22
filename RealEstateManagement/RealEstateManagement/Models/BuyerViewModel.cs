using DataAccess.EntityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RealEstateManagement.Models
{
    public class BuyerViewModel
    {
        [Key]
        public int BuyerId { get; set; }
        public List<PropertyViewModel> Properties { get; set; }
        public void GetProperties()
        {
            if( null != Properties ) Properties.Clear();
            using( RealEntities db = new RealEntities() )
            {
                buyer buyer = db.buyers.Where( b => b.buyer_id == BuyerId).FirstOrDefault();
                List<property> properties = null;
                if( buyer != null )
                {
                    properties = db.properties.Where( p => p.area <= buyer.max_area && p.area >= buyer.min_area && (p.min_price.plot_price + p.min_price.apartment_price) <= buyer.max_cost && ( p.min_price.plot_price + p.min_price.apartment_price ) >= buyer.min_cost).ToList<property>();
                }
                if( null == Properties ) Properties = new List<PropertyViewModel>();
                if( null != properties )
                {
                    foreach( var property in properties )
                    {
                        PropertyViewModel pm = new PropertyViewModel(property);
                        Properties.Add( pm );
                    }
                }
            }
        }
    }
}