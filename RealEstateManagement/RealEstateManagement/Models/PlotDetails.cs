using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace RealEstateManagement.Models
{
    public class PlotDetails
    {
        public long PlotId
        {
            get
            {
                return _plotId;
            }
        }

        public double Latitude { get; set; }
        public double Longitute { get; set; }

        private long _plotId;

        MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString);

        public PlotDetails( double lat, double lng )
        {
            Latitude = lat;
            Longitute = lng;
        }

        public PlotDetails()
        {
            Latitude = 0;
            Longitute = 0;
        }

        public bool SavePlot()
        {
            bool status = false;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("insert into plot_details ( plot_lat, plot_lng ) values ( @plot_lat, @plot_lng )", conn);
                cmd.Parameters.AddWithValue( "@plot_lat", Latitude );
                cmd.Parameters.AddWithValue( "@plot_lng", Longitute );
                cmd.ExecuteNonQuery();
                long id = cmd.LastInsertedId; 
                cmd.Dispose();
                if( id > 0 )
                {
                    _plotId = id;
                    status = true;
                }
                return status;
            }
            catch( MySqlException ex )
            {
                return status;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}