using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RealEstateManagement.Models;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;


namespace RealEstateManagement.Controllers
{
    public class PropertyController : Controller
    {
        // GET: Property
        public ActionResult Index()
        {
            PropertyViewModel PVM = new PropertyViewModel();
            PVM.GetPropertyCategories();
            return View( PVM );
        }

        public ActionResult Add()
        {
            PropertyViewModel PVM = new PropertyViewModel();
            PVM.GetPropertyCategories();
            return View( PVM );
        }

        [HttpPost]
        public ActionResult Create( PropertyViewModel model )
        {
            try
            {
                if( ModelState.IsValid )
                {
                    model.SellerId = Utils.GetSeller();
                    if( model.AddProperty() )
                    {
                        return RedirectToAction( "Index", "Seller" );

                    }
                    else
                        ModelState.AddModelError( "", "Property addition failed." );
                }
            }
            catch( Exception ex )
            {
            }
            return View( "Add", model );
        }

        public ActionResult StartBid( int PropertyId )
        {
            try
            {
                if( ModelState.IsValid )
                {
                    PropertyViewModel model = new PropertyViewModel(PropertyId);
                    if( model.ChangeStatus( PropertyStatus.Started ) )
                    {
                        return RedirectToAction( "Index", "Seller" );

                    }
                    else
                        ModelState.AddModelError( "", "Status change failed." );
                }
            }
            catch( Exception ex )
            {
            }
            return RedirectToAction( "Index", "Seller" );
        }

        public ActionResult CloseBid( int PropertyId )
        {
            try
            {
                if( ModelState.IsValid )
                {
                    PropertyViewModel model = new PropertyViewModel(PropertyId);
                    if( model.ChangeStatus( PropertyStatus.Closed ) )
                    {
                        return RedirectToAction( "Index", "Seller" );

                    }
                    else
                        ModelState.AddModelError( "", "Status change failed." );
                }
            }
            catch( Exception ex )
            {
            }
            return RedirectToAction( "Index", "Seller" );
        }

        public ActionResult ConfirmBid( int BidId )
        {
            try
            {
                if( ModelState.IsValid )
                {
                    BidVieModel model = new BidVieModel(BidId);
                    if( model.ConfirmBid() )
                    {
                        PropertyViewModel pvm = new PropertyViewModel(model.PropertyId);
                        pvm.ChangeStatus( PropertyStatus.Sold );
                        return RedirectToAction( "Index", "Seller" );

                    }
                    else
                        ModelState.AddModelError( "", "Status change failed." );
                }
            }
            catch( Exception ex )
            {
            }
            return RedirectToAction( "Index", "Seller" );
        }


        public ActionResult ViewBid( int PropertyId )
        {
            try
            {
                if( ModelState.IsValid )
                {
                    PropertyViewModel model = new PropertyViewModel(PropertyId);
                    if( model.GetBids() )
                    {
                        return View( model );

                    }
                    else
                        ModelState.AddModelError( "", "Status change failed." );
                }
            }
            catch( Exception ex )
            {
            }
            return RedirectToAction( "Index", "Seller" );
        }

        public ActionResult BidView( int PropertyId )
        {
            try
            {
                if( ModelState.IsValid )
                {
                    PropertyViewModel model = new PropertyViewModel(PropertyId);
                    if( model.GetHighestUniqueBid() )
                    {
                        return View( model );

                    }
                    else
                    {
                        model.Bid = null;
                        return View( model );
                    }
                }
            }
            catch( Exception ex )
            {
            }
            return RedirectToAction( "Index", "Buyer" );
        }

        public ActionResult PlaceBid( PropertyViewModel model )
        {
            try
            {
                if( ModelState.IsValid )
                {
                    BidVieModel bid = new BidVieModel();
                    bid.BuyerId = Utils.GetBuyer();
                    bid.PropertyId = model.PropertyId;
                    bid.Status = BidStatus.Pending;
                    bid.HousePrice = model.HousePrice;
                    bid.LandPrice = model.LandPrice;
                    if( bid.Place() )
                    {
                        return RedirectToAction( "Index", "Buyer" );

                    }
                    else
                        ModelState.AddModelError( "", "Property addition failed." );
                }
            }
            catch( Exception ex )
            {
            }
            return RedirectToAction( "Index", "Buyer" );
        }
    }
}