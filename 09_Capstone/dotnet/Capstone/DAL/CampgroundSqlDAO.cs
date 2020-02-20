using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Capstone.Models;

namespace Capstone.DAL
{
    public class CampgroundSqlDAO : ICampgroundDAO
    {
        private string connectionString;

        // Single Parameter Constructor
        public CampgroundSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public IList<Campground> ReadToListCampground()
        {
            List<Campground> campgrounds = new List<Campground>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM campground ORDER BY name", conn);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Campground camp = new Campground();

                        camp.CampgroundId = Convert.ToInt32(reader["campground_id"]);
                        camp.ParkId = Convert.ToInt32(reader["park_id"]);
                        camp.Name = Convert.ToString(reader["name"]);
                        camp.OpenFrom = Convert.ToInt32(reader["open_from_mm"]);
                        camp.OpenTo = Convert.ToInt32(reader["open_to_mm"]);
                        camp.DailyFee = Convert.ToDecimal(reader["daily_fee"]);

                        campgrounds.Add(camp);
                    }
                }
            }


            catch (SqlException ex)
            {

                Console.WriteLine("Error getting campgrounds");
                Console.WriteLine("The error was: " + ex.Message);
            }

            return campgrounds;
        }
    }
}
