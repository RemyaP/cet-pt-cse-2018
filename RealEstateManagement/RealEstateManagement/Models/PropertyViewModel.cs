using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateManagement.Models
{
    public class PropertyViewModel
    {
        [Key]
        public int PropertyId { get; set; }
        public double? Area { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public int SellerId { get; set; }
        public int Category { get; set; }

        public PropertyViewModel( property prop )
        {
            PropertyId = prop.property_id;
            Area = prop.area;
            Latitude = prop.latitude;
            Longitude = prop.longitude;
            SellerId = prop.seller_id;
            Category = prop.category;
        }

        public PropertyViewModel()
        {
        }

        [NotMapped]
        public SelectList Categories
        {
            get
            {
                return _categories;
            }
        }

        private static SelectList _categories = null;
        public void GetPropertyCategories()
        {
            if( null == _categories )
            {
                _categories = new SelectList( CategoryViewModel.GetCategories(), "Key", "Value" );
            }
        }

        public bool AddProperty()
        {
            using( RealEstateModel db = new RealEstateModel() )
            {
                property prop = new property();
                prop.property_id = PropertyId;
                prop.area = Area;
                prop.latitude = Latitude;
                prop.longitude = Longitude;
                prop.seller_id = SellerId;
                prop.category = Category;
                db.properties.Add( prop );
                db.SaveChanges();
            }
            return false;
        }
    }
}