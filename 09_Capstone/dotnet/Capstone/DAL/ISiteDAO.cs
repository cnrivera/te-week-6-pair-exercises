using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.DAL
{
    interface ISiteDAO
    {
        /// <summary>
        /// Returns a list of available reservations.
        /// </summary>
        /// <returns></returns>
        IList<Site> ReadToListSite(int campgroundId, DateTime inputStartDate, DateTime inputEndDate);
    }
}
