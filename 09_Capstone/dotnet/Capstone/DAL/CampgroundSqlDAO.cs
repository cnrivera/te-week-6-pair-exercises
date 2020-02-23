using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Capstone.Models;

namespace Capstone.DAL
{
    public class CampgroundSqlDAO : ICampgroundDAO
    {
        private readonly string connectionString;

        // Single Parameter Constructor
        public CampgroundSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public IList<Campground> ReadToListCampground(int parkid)
        {
            List<Campground> campgrounds = new List<Campground>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand($"SELECT * FROM campground WHERE park_id = {parkid}  ORDER BY name", conn);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Campground camp = new Campground
                        {
                            CampgroundId = Convert.ToInt32(reader["campground_id"]),
                            ParkId = Convert.ToInt32(reader["park_id"]),
                            Name = Convert.ToString(reader["name"]),
                            OpenFrom = Convert.ToInt32(reader["open_from_mm"]),
                            OpenTo = Convert.ToInt32(reader["open_to_mm"]),
                            DailyFee = Convert.ToDecimal(reader["daily_fee"])
                        };

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
