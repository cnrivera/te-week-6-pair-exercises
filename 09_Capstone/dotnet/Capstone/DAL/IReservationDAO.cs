using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.DAL
{
    interface IReservationDAO
    {
        /// <summary>
        /// Returns a list of available reservations.
        /// </summary>
        /// <returns></returns>
        IList<Reservation> ReadToListReservation(int campgroundId, DateTime inputStartDate, DateTime inputEndDate);
    }
}
