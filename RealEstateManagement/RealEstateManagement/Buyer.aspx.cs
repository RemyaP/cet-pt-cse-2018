using Newtonsoft.Json;
using RealEstateManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealEstateManagement
{
    public partial class Buyer : Page
    {
        protected void Page_Load( object sender, EventArgs e )
        {
        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public static string GetPlotDetails()
        {
            string response = "";
            try
            {
                List<PlotDetails> plotDetails = PlotDetails.GetAllPlots();
                response = JsonConvert.SerializeObject( plotDetails );
            }
            catch { }
            return response;
        }
    }
}