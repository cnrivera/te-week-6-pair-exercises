using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Capstone.Models;

namespace Capstone.DAL
{
    public class ReservationSqlDAO : IReservationDAO
    {

        private string connectionString;

        // Single Parameter Constructor
        public ReservationSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public int AddReservation(string inputNameReserve, int inputSiteReserve, DateTime inputStartDate, DateTime inputEndDate)
        {
           
            int id = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO reservation (site_id, name, from_date, to_date, create_date)VALUES (@inputSiteReserve, @inputNameReserve, @inputStartDate, @inputEndDate, GetDate())", conn);

                    cmd.Parameters.AddWithValue("@inputStartDate", inputStartDate);
                    cmd.Parameters.AddWithValue("@inputEndDate", inputEndDate);
                    cmd.Parameters.AddWithValue("@inputNameReserve", inputNameReserve);
                    cmd.Parameters.AddWithValue("@inputSiteReserve", inputSiteReserve);

                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("SELECT MAX(reservation_id) from reservation", conn);
                    id = Convert.ToInt32(cmd.ExecuteScalar());

                }
            }
            catch (SqlException ex)
            {

                Console.WriteLine("Error getting reservations");
                Console.WriteLine("The error was: " + ex.Message);
            }
            return id;
        }

   
    }


   
}


