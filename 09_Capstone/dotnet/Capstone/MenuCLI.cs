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
        
        // **Set variables for selected
        private int selectedParkId;
        private int selectedSiteId;
        private int selectedCampgroundId;
        private string selectedParkName;
        

        public MenuCLI(IParkDAO parkDAO, ISiteDAO siteDAO, ICampgroundDAO campgroundDAO, IReservationDAO reservationDAO)
        {
            this.parkDAO = parkDAO;
            this.siteDAO = siteDAO;
            this.campgroundDAO = campgroundDAO;
            this.reservationDAO = reservationDAO;

            RunMenu();
        }

        public void RunMenu()
        {

            PrintMenu();
            
            string input = Console.ReadLine().ToUpper();
            
            IList<Park> parkSelection = parkDAO.ViewAvailableParks();
            Console.Clear();

            if (input == "Q")
            {
                Environment.Exit(0);
            }
            else 
            {
            // ** changed order of menu and set up pInput for parsed int input
                // do error checking here - try/catch on parse?
                int pInput = int.Parse(input) - 1;
            
                selectedParkId = parkSelection[pInput].ParkId;
                selectedParkName = parkSelection[pInput].Name;
                Console.WriteLine("Park Information Screen");
                Console.WriteLine(parkSelection[pInput].Name + " National Park");
                Console.WriteLine("Location: " + parkSelection[pInput].Location);
                Console.WriteLine("Established: " +parkSelection[pInput].EstablishDate);
                Console.WriteLine("Area: " + parkSelection[pInput].Area + " sq km");
                Console.WriteLine("Annual Visitors: " + parkSelection[pInput].Visitors);
                Console.WriteLine();
                Console.WriteLine(parkSelection[pInput].Description);
                
            }


            PrintCampgroundMenu();
            
            RunCampgroundMenu();


        }


        public void PrintMenu()
        {
            selectedParkId = 0;
            selectedParkName = "";
            Console.WriteLine("Select a park for further details:");

            IList<Park> parkList = parkDAO.ViewAvailableParks();
            for (int i = 0; i < parkList.Count; i++)
            {
                Console.WriteLine(i + 1 + ") " + parkList[i].Name);
            }
            
            Console.WriteLine("Q) Quit");
        }

        public void PrintCampgroundMenu()
        {
            Console.WriteLine("Select a Command");
            Console.WriteLine("\t1) View Campgrounds");
            Console.WriteLine("\t2) Search for Reservation");
            Console.WriteLine("\t3) Return to Previous Menu");
        }

        // ** added new selectedParkId parameter to ReadToListCampground in CampgroundSqlDAO
        // ** added additional line at top
        public void ViewCampgrounds()
        {
            IList<Campground> campInfo = campgroundDAO.ReadToListCampground(selectedParkId);
            Console.WriteLine("Park Campgrounds"); 
            Console.WriteLine(selectedParkName + " National Park Campgrounds");
            Console.WriteLine("Name Open Close Daily Fee"); 
            
            for (int i = 0; i < campInfo.Count; i++)
            {
                Console.WriteLine(i+1 + campInfo[i].Name + " " + campInfo[i].OpenFrom + " " + campInfo[i].OpenTo + " " + campInfo[i].DailyFee);
            }
        }

        public void RunCampgroundMenu()
        {
            const string viewCampgrounds = "1";
            const string searchReservations = "2";
            const string previousString = "3";

            string input = Console.ReadLine();
            

            switch(input)
            {
                case "1":
                    ViewCampgrounds();
                    break;

                case "2":

                    break;

                case "3":
                    RunMenu();
                    break;

                default:
                    Console.WriteLine("Please select from the menu.");
                    break;




            }

        }

    }


}
