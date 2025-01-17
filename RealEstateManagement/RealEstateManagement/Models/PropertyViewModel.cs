﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using System.Data.Entity;
using DataAccess.EntityModels;
using System.IO;

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

    public enum PropertyStatus { Open = 1, Started, Closed, Sold }

    public class PropertyViewModel
    {
        [Key]
        public int PropertyId { get; set; }
        public string Name { get; set; }
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
        public double LandPrice { get; set; }
        public double HousePrice { get; set; }
        public List<string> Photos { get; set; }
        public List<BidVieModel> Bids { get; set; }
        public BidVieModel Bid { get; set; }

        [Display( Name = "Browse" )]
        public HttpPostedFileBase[] Images { get; set; }

        public PropertyViewModel( int PropertyId )
        {
            RealEntities db = new RealEntities();
            property prop = db.properties.Where( p => p.property_id == PropertyId ).FirstOrDefault();
            InitialiseProperty( prop );
        }

        public PropertyViewModel( property prop )
        {
            InitialiseProperty( prop );
        }

        private void InitialiseProperty( property prop )
        {
            PropertyId = prop.property_id;
            Name = prop.name;
            Area = prop.area;
            LandPrice = ( double )prop.min_price.plot_price;
            HousePrice = ( double )prop.min_price.apartment_price;
            Latitude = prop.latitude;
            Longitude = prop.longitude;
            SellerId = prop.seller_id;
            Category = prop.category;
            Type = prop.category1.type;
            Status = prop.propery_status.status;
            if( null != Photos ) Photos.Clear();
            else Photos = new List<string>();
            foreach( var image in prop.images )
            {
                byte[] photo = image.image1;
                string imageSrc = null;
                if( photo != null )
                {
                    MemoryStream ms = new MemoryStream();
                    ms.Write( photo, 0, photo.Length );
                    //ms.Write( photo, 78, photo.Length - 78 ); // strip out 78 byte OLE header (don't need to do this for normal images)
                    string imageBase64 = Convert.ToBase64String(ms.ToArray() );
                    imageSrc = string.Format( "data:image/gif;base64,{0}", imageBase64 );
                    Photos.Add( imageSrc );
                }
            }
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

        public bool ChangeStatus( PropertyStatus status )
        {
            try
            {
                RealEntities db = new RealEntities();
                property prop = db.properties.Where( p => p.property_id == PropertyId ).FirstOrDefault();
                prop.status = ( int )status;
                db.SaveChanges();
                return true;
            }
            catch( Exception ex )
            {
            }
            return false;
        }

        public bool GetBids()
        {
            if( null != Bids ) Bids.Clear();
            else Bids = new List<BidVieModel>();
            try
            {
                RealEntities db = new RealEntities();
                var bids = db.bids.Where( b => b.property_id == PropertyId );
                if( null != bids )
                {
                    foreach( var b in bids )
                    {
                        BidVieModel bid = new BidVieModel();
                        bid.BidId = b.bid_id;
                        bid.PropertyId = b.property_id;
                        bid.BuyerId = b.buyer_id;
                        bid.Status = ( BidStatus )b.status;
                        //bid.LandPrice = ( double )b.bid_price.plot_price;
                        //bid.HousePrice = ( double )b.bid_price.apartment_price;
                        //bid.BuyerName = b.buyer.user.first_name + " " + b.buyer.user.last_name;
                        //bid.BuyerAddress = b.buyer.user.address;
                        Bids.Add( bid );
                    }
                }
                return true;
            }
            catch( Exception ex )
            {
            }
            return false;
        }


        public bool GetHighestUniqueBid()
        {
            Bid = null;
            try
            {
                RealEntities db = new RealEntities();
                var bids = db.bids.Where(b => b.property_id == PropertyId).GroupBy( b => (b.bid_price.plot_price + b.bid_price.apartment_price) ).Where( b => (b.Count() == 1 )).FirstOrDefault();
                if( null == bids ) return false;
                var bid = bids.FirstOrDefault();
                if( null == bid ) return false;
                Bid = new BidVieModel();

                Bid.BidId = bid.bid_id;
                Bid.PropertyId = bid.property_id;
                Bid.BuyerId = bid.buyer_id;
                Bid.Status = ( BidStatus )bid.status;
                Bid.LandPrice = ( double )bid.bid_price.plot_price;
                Bid.HousePrice = ( double )bid.bid_price.apartment_price;
                Bid.BuyerName = bid.buyer.user.first_name + " " + bid.buyer.user.last_name;
                Bid.BuyerAddress = bid.buyer.user.address;
                return true;
            }
            catch( Exception ex )
            {
            }
            return false;
        }

        public bool AddProperty()
        {
            try
            {
                property prop = new property();
                List<Marker> markers = GetMarkers();
                RealEntities db = new RealEntities();
                prop.name = Name;
                prop.area = Area;
                prop.latitude = Latitude;
                prop.longitude = Longitude;
                prop.seller_id = SellerId;
                prop.category = Category;
                prop.status = 1;
                db.properties.Add( prop );
                db.SaveChanges();
                min_price price = new min_price();
                switch( prop.category )
                {
                    case 1:
                        {
                            price.plot_price = LandPrice;
                            price.apartment_price = 0;
                            break;
                        }
                    case 2:
                        {
                            price.plot_price = LandPrice;
                            price.apartment_price = HousePrice;
                            break;
                        }
                    case 3:
                        {
                            price.plot_price = 0;
                            price.apartment_price = HousePrice;
                            break;
                        }
                }
                price.property_id = prop.property_id;
                db.min_price.Add( price );
                db.SaveChanges();
                db.properties.Attach( prop );
                foreach( var marker in markers )
                {
                    RealEntities ldb = new RealEntities();
                    landmark lm = ldb.landmarks.Where( l => l.latitude == marker.Lat && l.longitude == marker.Lng && l.landmarktype == marker.Type).FirstOrDefault();
                    if( null == lm )
                    {
                        lm = new landmark();
                        lm.landmarktype = marker.Type;
                        lm.latitude = marker.Lat;
                        lm.longitude = marker.Lng;
                        lm.name = marker.Name;
                        ldb.landmarks.Add( lm );
                        ldb.SaveChanges();
                    }
                    if( 0 != lm.landmark_id )
                    {
                        landmark mark = db.landmarks.Where( l => l.landmark_id == lm.landmark_id).Single();
                        db.landmarks.Attach( mark );
                        prop.landmarks.Add( mark );
                    }
                }
                db.SaveChanges();

                foreach( HttpPostedFileBase image in Images )
                {
                    //Checking file is available to save.  
                    if( image != null )
                    {
                        image img = new image();
                        img.property_id = prop.property_id;
                        MemoryStream target = new MemoryStream();
                        image.InputStream.CopyTo( target );
                        img.image1 = target.ToArray();
                        db.images.Add( img );
                    }
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
            using( RealEntities db = new RealEntities() )
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
