using Capstone.DAL;
using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Models;

namespace Capstone
{
    class MenuCLI
    {
        private IParkDAO parkDAO;
        private ISiteDAO siteDAO;
        private ICampgroundDAO campgroundDAO;
        private IReservationDAO reservationDAO;

        public MenuCLI(IParkDAO parkDAO, ISiteDAO siteDAO, ICampgroundDAO campgroundDAO, IReservationDAO reservationDAO)
        {
            this.parkDAO = parkDAO;
            this.siteDAO = siteDAO;
            this.campgroundDAO = campgroundDAO;
            this.reservationDAO = reservationDAO;
        }

        public void ViewAvailableParksList()
        {
            IList<Park> parkList = parkDAO.ViewAvailableParks();
            foreach (Park park in parkList)
            {
                
            }
        }
    }


}
