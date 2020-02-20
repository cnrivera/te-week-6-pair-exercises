using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Models;

namespace Capstone.DAL
{
    interface ICampgroundDAO
    {
        /// <summary>
        /// Returns a list of all campgrounds.
        /// </summary>
        /// <returns></returns>
        IList<Campground> ReadToListCampground();
    }
}
