using RealEstateManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RealEstateManagement.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            UserViewModel UVM = new UserViewModel();
            return View( UVM );
        }

        // GET: User/Details/5
        public ActionResult Details( int id )
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create( UserViewModel model )
        {
            try
            {
                if( ModelState.IsValid )
                {
                    if( model.RegisterUser() )
                    {
                        System.Web.HttpContext.Current.Session["UserId"] = model.UserId;
                        if( UserType.Buyer == model.Type )
                        {
                            return RedirectToAction( "Index", "Buyer" );
                        }
                        else
                        {
                            return RedirectToAction( "Index", "Seller" );
                        }

                    }
                    else
                        ModelState.AddModelError( "", "Registration failed." );
                }
            }
            catch( Exception ex )
            {
            }
            return View( "Index", model );
        }

        public ActionResult Login()
        {
            UserViewModel UVM = new UserViewModel();
            return View( UVM );
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Login( UserViewModel model )
        {
            try
            {
                if( ModelState.IsValid )
                {
                    if( model.UserLogin() )
                    {
                        System.Web.HttpContext.Current.Session["UserId"] = model.UserId;
                        if( UserType.Buyer == model.Type )
                        {
                            return RedirectToAction( "Index", "Buyer" );
                        }
                        else
                        {
                            return RedirectToAction( "Index", "Seller" );
                        }

                    }
                    else
                        ModelState.AddModelError( "", "Registration failed." );
                }
            }
            catch( Exception ex )
            {
            }
            return View( "Index", model );
        }

        // GET: User/Delete/5
        public ActionResult Delete( int id )
        {
            return View();
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete( int id, FormCollection collection )
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction( "Index" );
            }
            catch
            {
                return View();
            }
        }
    }
}
