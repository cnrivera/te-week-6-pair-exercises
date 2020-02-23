using Capstone.DAL;
using Capstone.Models;
using System;
using System.Collections.Generic;

namespace Capstone
{
    class MenuCLI
    {
        private readonly IParkDAO parkDAO;
        private readonly ISiteDAO siteDAO;
        private readonly ICampgroundDAO campgroundDAO;
        private readonly IReservationDAO reservationDAO;

        private string parkInput;
        private int selectedParkId;
        private int selectedCampgroundId;
        private string selectedParkName;
        private DateTime inputStartDate;
        private DateTime inputEndDate;
        private string selectedCampgroundName;
        private decimal selectedCampgroundCost;
        private int inputSiteReserve;
        private string inputNameReserve;
        private TimeSpan numDays;
        private double intDays;


        public MenuCLI(IParkDAO parkDAO, ISiteDAO siteDAO, ICampgroundDAO campgroundDAO, IReservationDAO reservationDAO)
        {
            this.parkDAO = parkDAO;
            this.siteDAO = siteDAO;
            this.campgroundDAO = campgroundDAO;
            this.reservationDAO = reservationDAO;

            // Start main menu
            RunParkMenu();
        }

        // // Create menu of available parks 
        public void RunParkMenu()
        {
            Console.WriteLine("View Parks Interface");
            Console.WriteLine();
            selectedParkId = 0;
            Console.WriteLine("Select a park for further details:");

            IList<Park> parkList = parkDAO.ViewAvailableParks();
            for (int i = 0; i < parkList.Count; i++)
            {
                Console.WriteLine(i + 1 + ") " + parkList[i].Name);
            }

            Console.WriteLine("Q) Quit");

            parkInput = Console.ReadLine().ToUpper();
            DisplayParkInfo(parkInput);
        }

        // Display details for selected park
        public void DisplayParkInfo(string parkInput)
        {

            IList<Park> parkSelection = parkDAO.ViewAvailableParks();
            Console.Clear();


            if (parkInput == "Q")
            {
                Environment.Exit(0);
            }
            else
            {

                try
                {
                    int pInput = int.Parse(parkInput) - 1;

                    selectedParkId = parkSelection[pInput].ParkId;
                    selectedParkName = parkSelection[pInput].Name;
                    Console.WriteLine("Park Information Screen");
                    Console.WriteLine();
                    Console.WriteLine(parkSelection[pInput].Name + " National Park");
                    Console.WriteLine("Location: " + parkSelection[pInput].Location);
                    Console.WriteLine("Established: " + parkSelection[pInput].EstablishDate);
                    Console.WriteLine("Area: " + parkSelection[pInput].Area + " sq km");
                    Console.WriteLine("Annual Visitors: " + parkSelection[pInput].Visitors);
                    Console.WriteLine();
                    Console.WriteLine(parkSelection[pInput].Description);
                    Console.WriteLine();
                    RunCampgroundMenu();
                }


                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message); 
                    Console.WriteLine("Could not read input. Please try again.");
                    Console.WriteLine();
                    RunParkMenu();

                }
            }
            
        }

        // View campgrounds for selected park
        public void ViewCampgrounds()
        {
            Console.Clear();
            IList<Campground> campInfo = campgroundDAO.ReadToListCampground(selectedParkId);
            Console.WriteLine("Park Campgrounds");
            Console.WriteLine(selectedParkName + " National Park Campgrounds");
            Console.WriteLine("Name Open Close Daily Fee");

            for (int i = 0; i < campInfo.Count; i++)
            {
                Console.WriteLine(i + 1 + campInfo[i].Name + " " + campInfo[i].OpenFrom + " " + campInfo[i].OpenTo + " " + campInfo[i].DailyFee);
            }
            Console.WriteLine();

            // Run submenu
            string input = "";
            while (input != "1" && input != "2")
            {
                Console.WriteLine("Select a Command");
                Console.WriteLine("1)\tSearch for Available Reservations");
                Console.WriteLine("2)\tReturn to Previous Menu");
                input = CLIHelper.GetString("");
            }

            if (input == "2")
            {
                Console.Clear();
                DisplayParkInfo(parkInput);
                RunCampgroundMenu();
            }
            else if (input == "1")
            {
                Console.Clear();
                SearchReservations();
            }

        }

        // Menu for selected park 
        public void RunCampgroundMenu()
        {
            Console.WriteLine("Select a Command");
            Console.WriteLine("\t1) View Campgrounds");
            Console.WriteLine("\t2) Search for Reservation");
            Console.WriteLine("\t3) Return to Previous Menu");
           
            string input = Console.ReadLine();


            switch (input)
            {
                case "1":
                    ViewCampgrounds();
                    break;

                case "2":
                    SearchReservations();
                    break;

                case "3":
                    Console.Clear();
                    RunParkMenu();
                    break;

                default:
                    Console.Clear();
                    Console.WriteLine("Invalid response. Please select from the menu.");
                    RunCampgroundMenu();
                    
                    
                    break;
            }
        }

        // Search for and/or make site reservations
        public void SearchReservations()
        {
            selectedCampgroundId = 0;
            selectedCampgroundName = "";
            selectedCampgroundCost = 0;



            //Search for Available Reservation 
            Console.Clear();
            IList<Campground> campInfo = campgroundDAO.ReadToListCampground(selectedParkId);
            Console.WriteLine("Park Campgrounds");
            Console.WriteLine(selectedParkName + " National Park Campgrounds");
            Console.WriteLine("Name Open Close Daily Fee");

            for (int i = 0; i < campInfo.Count; i++)
            {
                Console.WriteLine(i + 1 + campInfo[i].Name + " " + campInfo[i].OpenFrom + " " + campInfo[i].OpenTo + " " + campInfo[i].DailyFee);
            }
            Console.WriteLine();


            string inputCampground = CLIHelper.GetString("Select a campground (enter Q to quit and return to main menu)");

            if (inputCampground.ToUpper() == "Q")
            {
                RunParkMenu();
            }
            try
            {
                int pInputCampground = int.Parse(inputCampground) - 1;

                selectedCampgroundId = campInfo[pInputCampground].CampgroundId;
                selectedCampgroundName = campInfo[pInputCampground].Name;
                selectedCampgroundCost = campInfo[pInputCampground].DailyFee;
            }

            catch (Exception)
            {

                Console.WriteLine("Not a valid campground; please press any key to try again");
                Console.ReadLine();
                SearchReservations();

            }

            bool datesAreValid = false;
            while (!datesAreValid) 
            { 
                inputStartDate = CLIHelper.GetDate("What is the arrival date ? mm / dd / yyyy");
                inputEndDate = CLIHelper.GetDate("What is the departure date ? mm / dd / yyyy");

                // Calculate number of days for reservation
                numDays = inputEndDate.Subtract(inputStartDate);
                intDays = numDays.TotalDays;
                if (inputStartDate < DateTime.Now)
                {
                    Console.WriteLine();
                    Console.WriteLine("Arrival/departure dates must be in the future. We do not have time traveling capabilities.\nPlease try new dates.");
                }
                else if (intDays < 1)
                {
                    Console.WriteLine("Departure date must be after arrival date. \nPlease try new dates.");
                }
                else
                {
                    datesAreValid = true;
                }
            }




            // Display results matching search
            IList<Site> siteAvailList = siteDAO.ReadToListSite(selectedCampgroundId, inputStartDate, inputEndDate);
            Console.WriteLine("Results Matching Your Search Criteria");
            if (siteAvailList.Count == 0)
            {

                Console.WriteLine("No sites are available for your dates. Would you like to enter alternate dates? Y/N");
                string yesOrNo;
                bool isValid = false;

                do
                {
                    yesOrNo = Console.ReadLine();
                    if (yesOrNo.ToUpper() != "Y" && yesOrNo.ToUpper() != "N")
                    {
                        Console.WriteLine("Please enter Y or N.");
                    }
                    else
                    {
                        isValid = true;
                    }

                }
                while (!isValid);

                if (yesOrNo.ToUpper() == "N")
                {
                    Console.Clear();
                    RunParkMenu();
                }
                else if (yesOrNo.ToUpper() == "Y")
                {
                    SearchReservations();
                }

                }
                else
                {

                    Console.WriteLine($"Campground\tSiteNo.\tMax Occup.\tAccessible?\tRV Length\tUtilities Available?\tTotal Cost for {intDays} days");
                    List<int> validSiteIds = new List<int>();
                    foreach (Site slot in siteAvailList)
                    {
                        decimal campTotal = selectedCampgroundCost * (decimal)intDays;
                        Console.WriteLine(selectedCampgroundName + "\t" + slot.SiteId + "\t" + slot.MaxOccupancy + "\t" + slot.Accessible + "\t" + slot.MaxRvLength + "\t" + slot.Utilities + "\t" + campTotal);
                        validSiteIds.Add(slot.SiteId);
                    }

                // check input against site numbers in the list

                bool inputInvalid = true;
                while (inputInvalid)
                {
                    inputSiteReserve = CLIHelper.GetInteger("Which site should be reserved? Enter site number from above list or 0 to start over: ");
                    if (inputSiteReserve == 0)
                    {
                        SearchReservations();
                        break;
                    }
                    else if (validSiteIds.Contains(inputSiteReserve))
                    {
                        inputInvalid = false;
                    }
                }

                inputNameReserve = CLIHelper.GetString("What name should the reservation be made under?");

                // Add reservation
                int id = reservationDAO.AddReservation(inputNameReserve, inputSiteReserve, inputStartDate, inputEndDate);

                if (id != 0)
                {
                    Console.WriteLine($"The reservation has been made, and the confirmation id is {id}");

                }
                else
                {
                    Console.WriteLine("Reservation unsuccessful.");
                    //option to go back
                }
                string inputContinue = "";
                while (inputContinue != "1" && inputContinue != "0")
                {
                    inputContinue = CLIHelper.GetString("Enter 1 to return to the main menu or 0 to quit");
                }
                if (inputContinue == "0")
                {
                    Environment.Exit(0);
                }
                else if (inputContinue == "1")
                {
                    Console.Clear();
                    RunParkMenu();
                }

            }

        }

    }

}
