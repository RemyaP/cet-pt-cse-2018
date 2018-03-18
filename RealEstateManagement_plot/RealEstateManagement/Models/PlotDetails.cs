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
        public double Longitude { get; set; }

        private long _plotId;

        protected static MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString);

        public PlotDetails( double lat, double lng )
        {
            Latitude = lat;
            Longitude = lng;
        }

        private PlotDetails(long id, double lat, double lng )
        {
            Latitude = lat;
            Longitude = lng;
            _plotId = id;
        }

        public PlotDetails()
        {
            Latitude = 0;
            Longitude = 0;
        }

        public bool SavePlot()
        {
            bool status = false;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("insert into plot_details ( plot_lat, plot_lng ) values ( @plot_lat, @plot_lng )", conn);
                cmd.Parameters.AddWithValue( "@plot_lat", Latitude );
                cmd.Parameters.AddWithValue( "@plot_lng", Longitude );
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


        public static List<PlotDetails> GetAllPlots()
        {
            List < PlotDetails > plotDetails = new List < PlotDetails >();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from plot_details", conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while( rdr.Read() )
                {
                    var id = rdr.GetInt64("plot_id");
                    var lat = rdr.GetDouble("plot_lat");
                    var lng = rdr.GetDouble("plot_lng");
                    plotDetails.Add( new PlotDetails(id, lat, lng));
                }
                return plotDetails;
            }
            catch( MySqlException ex )
            {
                return plotDetails;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}