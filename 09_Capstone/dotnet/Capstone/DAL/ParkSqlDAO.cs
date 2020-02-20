﻿using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Models;
using Capstone.DAL;
using System.Data.SqlClient;

namespace Capstone.DAL
{
    class ParkSqlDAO : IParkDAO
    {
        private string connectionString;

        // Single Parameter Constructor
        public ParkSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public IList<Park> ViewAvailableParks()
        {
            List<Park> parks = new List<Park>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM park ORDER BY name", conn);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Park park = new Park();

                        park.ParkId = Convert.ToInt32(reader["park_id"]);
                        park.Name = Convert.ToString(reader["name"]);
                        park.Location = Convert.ToString(reader["location"]);
                        park.EstablishDate = Convert.ToDateTime(reader["establish_date"]);
                        park.Area = Convert.ToInt32(reader["area"]);
                        park.Visitors = Convert.ToInt32(reader["visitors"]);
                        park.Description = Convert.ToString(reader["description"]);

                        parks.Add(park);
                    }
                }
            }


            catch (SqlException ex)
            {

                Console.WriteLine("Error getting parks list");
                Console.WriteLine("The error was: " + ex.Message);
            }

            return parks;
        }
    }
}
