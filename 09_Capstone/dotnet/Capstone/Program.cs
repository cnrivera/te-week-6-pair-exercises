using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using Capstone.DAL;

namespace Capstone
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get the connection string from the appsettings.json file
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            string connectionString = configuration.GetConnectionString("campground-tiny");

            IParkDAO parkDAO = new ParkSqlDAO(connectionString);
            //IReservationDAO reservationDAO = new ReservationSqlDAO(connectionString);
            //ISiteDAO siteDAO = new SiteSqlDAO(connectionString);
            //ICampgroundDAO campgroundDAO = new CampgroundSqlDAO(connectionString);

            Console.WriteLine(parkDAO.ViewAvailableParks());
        }
    }
}
