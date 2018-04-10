using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RealEstateManagement.Models
{
    public enum SellerType
    {
        Seller,
        BuyerSeller
    }
    public class SellerViewModel
    {
        [Key]
        public int SellerId { get; set; }
        public SellerType SellerType { get; set; }
        public List<PropertyViewModel> Properties { get; set; }
        public void GetProperties()
        {
            if( null != Properties ) Properties.Clear();
            using( RealEstateModel db = new RealEstateModel() )
            {
                var properties = db.properties.Where( p => p.seller_id == SellerId ).ToList<property>();
                if( null == Properties ) Properties = new List<PropertyViewModel>();
                foreach( var property in properties )
                {
                    PropertyViewModel pm = new PropertyViewModel(property);
                    Properties.Add( pm );
                }
            }
        }
    }
}