using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.DAL
{
    interface IReservationDAO
    {
        /// <summary>
        /// Adds a reservation to the database.
        /// </summary>
        /// <returns></returns>
        int AddReservation(string inputNameReserve, int inputSiteReserve, DateTime inputStartDate, DateTime inputEndDate);

      
    }
}
