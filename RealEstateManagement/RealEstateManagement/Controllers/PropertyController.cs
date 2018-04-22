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
    }
}