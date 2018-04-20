using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using System.Data.Entity;

namespace RealEstateManagement.Models
{
    class Marker
    {
        public int Type { get; set; }
        public double Lat
        {
            get
            {
                return Math.Round( _lat, 13 );
            }
            set
            {
                _lat = value;
            }
        }
        public double Lng
        {
            get
            {
                return Math.Round( _lng, 13 );
            }
            set
            {
                _lng = value;
            }
        }
        public string Name
        {
            get
            {
                if( _name.Length <= 100 ) return _name;
                return _name.Substring( 0, 100 ); ;
            }
            set
            {
                _name = value;
            }
        }


        private double _lat;
        private double _lng;
        private string _name;
    }
    public class PropertyViewModel
    {
        [Key]
        public int PropertyId { get; set; }
        public double? Area { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public int SellerId { get; set; }
        public int Category { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public string church { get; set; }
        public string school { get; set; }
        public string train_station { get; set; }
        public string hindu_temple { get; set; }
        public string hospital { get; set; }
        public string mosque { get; set; }

        public PropertyViewModel( property prop )
        {
            PropertyId = prop.property_id;
            Area = prop.area;
            Latitude = prop.latitude;
            Longitude = prop.longitude;
            SellerId = prop.seller_id;
            Category = prop.category;
            Type = prop.category1.type;
            Status = prop.status1.status1;
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
            try
            {
                property prop = new property();
                List<Marker> markers = GetMarkers();
                RealEstateModel db = new RealEstateModel();
                prop.area = Area;
                prop.latitude = Latitude;
                prop.longitude = Longitude;
                prop.seller_id = SellerId;
                prop.category = Category;
                prop.status = 1;
                db.properties.Add( prop );
                //db.SaveChanges();
                db.properties.Attach( prop );
                foreach( var marker in markers )
                {
                    long landmark_id = 0;
                    landmark lm = db.landmarks.Where( l => l.latitude == marker.Lat && l.longitude == marker.Lng && l.landmarktype == marker.Type).FirstOrDefault();
                    if( null == lm )
                    {
                        lm = new landmark();
                        lm.landmarktype = marker.Type;
                        lm.latitude = marker.Lat;
                        lm.longitude = marker.Lng;
                        lm.name = marker.Name;
                        db.landmarks.Add( lm );
                        db.landmarks.Attach( lm );
                       // db.SaveChanges();
                        //landmark_id = lm.landmark_id;
                    }
                    prop.landmarks.Add( lm );
                }
                db.SaveChanges();
                return true;
            }
            catch( Exception ex )
            {
            }
            return false;
        }

        private List<Marker> GetMarkers()
        {
            using( RealEstateModel db = new RealEstateModel() )
            {
                List<Marker> markers = new List<Marker>();
                if( null != church )
                {
                    foreach( var marker in JsonConvert.DeserializeObject<List<Marker>>( church ) )
                    {
                        landmarktype lt = db.landmarktypes.Where(t => t.type_name == "Church").FirstOrDefault();
                        if( null == lt ) continue;
                        marker.Type = lt.type_id;
                        markers.Add( marker );
                    }
                }
                if( null != school )
                {
                    foreach( var marker in JsonConvert.DeserializeObject<List<Marker>>( school ) )
                    {
                        landmarktype lt = db.landmarktypes.Where(t => t.type_name == "School").FirstOrDefault();
                        if( null == lt ) continue;
                        marker.Type = lt.type_id;
                        markers.Add( marker );
                    }
                }
                if( null != train_station )
                {
                    foreach( var marker in JsonConvert.DeserializeObject<List<Marker>>( train_station ) )
                    {
                        landmarktype lt = db.landmarktypes.Where(t => t.type_name == "Railway").FirstOrDefault();
                        if( null == lt ) continue;
                        marker.Type = lt.type_id;
                        markers.Add( marker );
                    }
                }
                if( null != hindu_temple )
                {
                    foreach( var marker in JsonConvert.DeserializeObject<List<Marker>>( hindu_temple ) )
                    {
                        landmarktype lt = db.landmarktypes.Where(t => t.type_name == "Temple").FirstOrDefault();
                        if( null == lt ) continue;
                        marker.Type = lt.type_id;
                        markers.Add( marker );
                    }
                }
                if( null != hospital )
                {
                    foreach( var marker in JsonConvert.DeserializeObject<List<Marker>>( hospital ) )
                    {
                        landmarktype lt = db.landmarktypes.Where(t => t.type_name == "Hospital").FirstOrDefault();
                        if( null == lt ) continue;
                        marker.Type = lt.type_id;
                        markers.Add( marker );
                    }
                }
                if( null != mosque )
                {
                    foreach( var marker in JsonConvert.DeserializeObject<List<Marker>>( mosque ) )
                    {
                        landmarktype lt = db.landmarktypes.Where(t => t.type_name == "Mosque").FirstOrDefault();
                        if( null == lt ) continue;
                        marker.Type = lt.type_id;
                        markers.Add( marker );
                    }
                }
                return markers;
            }
        }
    }
}
