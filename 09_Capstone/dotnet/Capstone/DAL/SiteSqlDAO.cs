using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Capstone.Models;

namespace Capstone.DAL
{
    public class SiteSqlDAO : ISiteDAO
    {
        private string connectionString;

        // Single Parameter Constructor
        public SiteSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public IList<Site> ReadToListSite(int campgroundID, DateTime inputStartDate, DateTime inputEndDate)
        {
            List<Site> sites = new List<Site>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT DISTINCT site.* from site JOIN reservation ON reservation.site_id = site.site_id AND campground_id = @campgroundId WHERE site.site_id NOT IN (SELECT site_id FROM reservation WHERE from_date <= @inputEndDate AND to_date >= @inputStartDate) ", conn);

                    cmd.Parameters.AddWithValue("@inputStartDate", inputStartDate);
                    cmd.Parameters.AddWithValue("@inputEndDate", inputEndDate);
                    cmd.Parameters.AddWithValue("@campgroundId", campgroundID);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                   
                    {
                        Site site = new Site();

                        site.SiteId = Convert.ToInt32(reader["site_id"]);
                        site.CampgroundId = Convert.ToInt32(reader["campground_id"]);
                        site.SiteNumber = Convert.ToInt32(reader["site_number"]);
                        site.MaxOccupancy = Convert.ToInt32(reader["max_occupancy"]);
                        site.Accessible = Convert.ToBoolean(reader["accessible"]);
                        site.MaxRvLength = Convert.ToInt32(reader["max_rv_length"]);
                        site.Utilities = Convert.ToBoolean(reader["utilities"]);

                        sites.Add(site);
                    }
                }
            }


            catch (SqlException ex)
            {

                Console.WriteLine("Error getting campgrounds");
                Console.WriteLine("The error was: " + ex.Message);
            }

            return sites;
        }
    }
}
