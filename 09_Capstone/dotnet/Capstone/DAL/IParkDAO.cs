using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Models;

namespace Capstone.DAL
{
    interface IParkDAO
    {
        /// <summary>
        /// Returns a list of all parks.
        /// </summary>
        /// <returns></returns>
        IList<Park> ViewAvailableParks();
    }
}
