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
        
        private int selectedParkId;
        private int selectedSiteId;
        private int selectedCampgroundId;
        private string selectedParkName;
        private DateTime inputStartDate;
        private DateTime inputEndDate;
        private string selectedCampgroundName;
        private decimal selectedCampgroundCost;


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

            PrintParkMenu();

            RunParkMenu();

            PrintCampgroundMenu();
            
            RunCampgroundMenu();

        }


        public void PrintParkMenu()
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

        public void RunParkMenu()
        {

            string input = Console.ReadLine().ToUpper();

            IList<Park> parkSelection = parkDAO.ViewAvailableParks();
            Console.Clear();

            if (input == "Q")
            {
                Environment.Exit(0);
            }
            else
            {

                try
                {
                int pInput = int.Parse(input) - 1;

                selectedParkId = parkSelection[pInput].ParkId;
                selectedParkName = parkSelection[pInput].Name;
                Console.WriteLine("Park Information Screen");
                Console.WriteLine(parkSelection[pInput].Name + " National Park");
                Console.WriteLine("Location: " + parkSelection[pInput].Location);
                Console.WriteLine("Established: " + parkSelection[pInput].EstablishDate);
                Console.WriteLine("Area: " + parkSelection[pInput].Area + " sq km");
                Console.WriteLine("Annual Visitors: " + parkSelection[pInput].Visitors);
                Console.WriteLine();
                Console.WriteLine(parkSelection[pInput].Description);
                }
                //figure out how to go back to first menu without running next menu
                
                catch (Exception ex)
                {
                    Console.WriteLine("Could not read input; please try again");
                    Console.WriteLine(ex.Message);
                    PrintParkMenu();
                }

            }
        }


        public void PrintCampgroundMenu()
        {
            Console.WriteLine("Select a Command");
            Console.WriteLine("\t1) View Campgrounds");
            Console.WriteLine("\t2) Search for Reservation");
            Console.WriteLine("\t3) Return to Previous Menu");
        }

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
                    SearchReservations();
                    break; 

                case "3":
                    RunMenu();
                    break;

                default:
                    Console.WriteLine("Please select from the menu.");
                    break;




            }

        }

        public void SearchReservations()
        {
            Console.WriteLine("Select a Command");
            Console.WriteLine("1)\tSearch for Available Reservations");
            Console.WriteLine("2)\tReturn to Previous Screen");
            string input = Console.ReadLine();

            if (input == "2") 
            {
                //return to menu
            }
            else if (input == "1")
            {

                //Search for Available Reservation 
                ViewCampgrounds();
                Console.WriteLine("Which campground(enter 0 to cancel) ?");
                string inputCampground = Console.ReadLine();
                IList<Campground> campInfo = campgroundDAO.ReadToListCampground(selectedParkId);
                try
                {
                    int pInputCampground = int.Parse(inputCampground) - 1;

                    selectedCampgroundId = campInfo[pInputCampground].CampgroundId;
                    selectedCampgroundName = campInfo[pInputCampground].Name;
                    selectedCampgroundCost = campInfo[pInputCampground].DailyFee;
                }
                
                catch (Exception ex)
                {
                    Console.WriteLine("Could not read input; please try again");
                    Console.WriteLine(ex.Message);
                    //get back to right place
                }

                Console.WriteLine("What is the arrival date ? mm / dd / yyyy");
                inputStartDate = Convert.ToDateTime(Console.ReadLine());
                Console.WriteLine("What is the departure date ? mm / dd / yyyy");
                inputEndDate = Convert.ToDateTime(Console.ReadLine());

                IList<Site> siteAvailList = siteDAO.ReadToListSite(selectedCampgroundId, inputStartDate, inputEndDate);
                Console.WriteLine("Results Matching Your Search Criteria");
                Console.WriteLine("Campground\tSiteNo.\tMax Occup.\tAccessible?\tRV Length\tUtilities Available?\tDaily Cost");
                foreach (Site slot in siteAvailList)
                {
                    Console.WriteLine(selectedCampgroundName + "\t" + slot.SiteId + "\t" + slot.MaxOccupancy + "\t" + slot.Accessible + "\t" + slot.MaxRvLength + "\t" + slot.Utilities + "\t" + selectedCampgroundCost);
                }

            }

        }

    }


}
