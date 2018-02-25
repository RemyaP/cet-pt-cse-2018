using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RealEstateManagement.Models;

namespace RealEstateManagement
{
    public partial class Seller : Page
    {
        private PlotDetails _plotDetails;
        protected void Page_Load( object sender, EventArgs e )
        {

        }

        protected void Add_Click( object sender, EventArgs e )
        {
            double lati = Convert.ToDouble( lat.Value);
            double longi = Convert.ToDouble(lng.Value);
            _plotDetails = new PlotDetails( lati, longi );
            bool status = _plotDetails.SavePlot();
            if( status )
            {
                ShowMessage( "Registered successfuly...!" );
            }
            else
            {
                ShowMessage( "Registration failed...!" );
            }
        }

        void ShowMessage( string msg )
        {
            string script = "alert(\"" + msg + "\");";
            ScriptManager.RegisterClientScriptBlock( this, GetType(),
                                  "ServerControlScript", script, true );
        }
    }
}