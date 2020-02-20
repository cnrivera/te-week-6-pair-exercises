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

        public IList<Reservation> ReadToListReservation()
        {
            List<Reservation> reservations = new List<Reservation>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM reservation ORDER BY name", conn);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Reservation reserve = new Reservation();

                        reserve.ReservationId = Convert.ToInt32(reader["reservation_id"]);
                        reserve.SiteId = Convert.ToInt32(reader["site_id"]);
                        reserve.Name = Convert.ToString(reader["name"]);
                        reserve.FromDate = Convert.ToDateTime(reader["from_date"]);
                        reserve.ToDate = Convert.ToDateTime(reader["to_date"]);
                        reserve.CreateDate = Convert.ToDateTime(reader["create_date"]);
                       

                        reservations.Add(reserve);
                    }
                }
            }


            catch (SqlException ex)
            {

                Console.WriteLine("Error getting reservations");
                Console.WriteLine("The error was: " + ex.Message);
            }

            return reservations;
        }
    }
}
