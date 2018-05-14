using DataAccess.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstateManagement.Models
{
    public enum BidStatus { Pending, Approved, Rejected };
    public class BidVieModel
    {
        public int BidId { get; set; }
        public int PropertyId { get; set; }
        public int BuyerId { get; set; }
        public BidStatus Status { get; set; }
        public double LandPrice { get; set; }
        public double HousePrice { get; set; }
        public string BuyerName { get; set; }
        public string BuyerAddress { get; set; }
        public string Type { get; set; }

        public BidVieModel()
        { }

        public BidVieModel( int bidId )
        {
            RealEntities db = new RealEntities();
            var bid = db.bids.Where( b => b.bid_id == bidId ).FirstOrDefault();
            if( null != bid )
            {
                BidId = bid.bid_id;
                PropertyId = bid.property_id;
                BuyerId = bid.buyer_id;
                Status = ( BidStatus )bid.status;
                LandPrice = ( double )bid.bid_price.plot_price;
                HousePrice = ( double )bid.bid_price.apartment_price;
                BuyerName = bid.buyer.user.first_name + " " + bid.buyer.user.last_name;
                BuyerAddress = bid.buyer.user.address;
            }
        }

        public bool ConfirmBid()
        {
            try
            {
                RealEntities db = new RealEntities();
                var bids = db.bids.Where( b => b.property_id == PropertyId && b.bid_id != BidId);
                if( null != bids )
                {
                    foreach( var b in bids )
                    {
                        b.status = ( int )BidStatus.Rejected;
                    }
                }
                var bid = db.bids.Where( b => b.bid_id == BidId).FirstOrDefault();
                if( null != bid )
                {
                    bid.status = ( int )BidStatus.Approved;
                }
                db.SaveChanges();
                return true;
            }
            catch( Exception ex )
            {
            }
            return false;
        }

        internal bool Place()
        {
            try
            {
                RealEntities db = new RealEntities();
                bid bid = new bid();
                bid.property_id = PropertyId;
                bid.buyer_id = BuyerId;
                bid.status = ( int )Status;
                db.bids.Add( bid );
                db.SaveChanges();
                bid_price bp = new bid_price();
                bp.bid_id = bid.bid_id;
                bp.apartment_price = HousePrice;
                bp.plot_price = LandPrice;
                db.bid_price.Add( bp );
                db.SaveChanges();
                return true;
            }
            catch( Exception ex )
            {
            }
            return false;
        }
    }
}
