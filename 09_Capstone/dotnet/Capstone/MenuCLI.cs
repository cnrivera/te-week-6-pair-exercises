using Capstone.DAL;
using Capstone.Models;
using System;
using System.Collections.Generic;

namespace Capstone
{
    class MenuCLI
    {
        private IParkDAO parkDAO;
        private ISiteDAO siteDAO;
        private ICampgroundDAO campgroundDAO;
        private IReservationDAO reservationDAO;

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

            RunMenu();
        }

        public void RunMenu()
        {
            RunParkMenu();

            PrintCampgroundMenu();

            RunCampgroundMenu();
        }


        public void RunParkMenu()
        {
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
            

            //IList<Park> parkSelection = parkDAO.ViewAvailableParks();
            //Console.Clear();

            //if (input == "Q")
            //{
            //    Environment.Exit(0);
            //}
            //else
            //{

            //    try
            //    {
            //        int pInput = int.Parse(input) - 1;

            //        selectedParkId = parkSelection[pInput].ParkId;
            //        selectedParkName = parkSelection[pInput].Name;
            //        Console.WriteLine("Park Information Screen");
            //        Console.WriteLine();
            //        Console.WriteLine(parkSelection[pInput].Name + " National Park");
            //        Console.WriteLine("Location: " + parkSelection[pInput].Location);
            //        Console.WriteLine("Established: " + parkSelection[pInput].EstablishDate);
            //        Console.WriteLine("Area: " + parkSelection[pInput].Area + " sq km");
            //        Console.WriteLine("Annual Visitors: " + parkSelection[pInput].Visitors);
            //        Console.WriteLine();
            //        Console.WriteLine(parkSelection[pInput].Description);
            //        Console.WriteLine();
            //    }


            //    catch (Exception ex)
            //    {
            //        Console.WriteLine("Could not read input; please try again");
            //        Console.WriteLine();
            //        Console.WriteLine(ex.Message);

            //    }

        }


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
                }


                catch (Exception ex)
                {
                    Console.WriteLine("Could not read input; please try again");
                    Console.WriteLine();
                    Console.WriteLine(ex.Message);

                }
            }
        }

        public void PrintCampgroundMenu()
        {
            //Console.WriteLine("Select a Command");
            //Console.WriteLine("\t1) View Campgrounds");
            //Console.WriteLine("\t2) Search for Reservation");
            //Console.WriteLine("\t3) Return to Previous Menu");
        }

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

            Console.WriteLine("Select a Command");
            Console.WriteLine("1)\tSearch for Available Reservations");
            Console.WriteLine("2)\tReturn to Previous Menu");
            string input = Console.ReadLine();

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

        public void RunCampgroundMenu()
        {
            Console.WriteLine("Select a Command");
            Console.WriteLine("\t1) View Campgrounds");
            Console.WriteLine("\t2) Search for Reservation");
            Console.WriteLine("\t3) Return to Previous Menu");

            const string viewCampgrounds = "1";
            const string searchReservations = "2";
            const string previousString = "3";

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
                    RunMenu();
                    break;

                default:
                    Console.WriteLine("Please select from the menu.");
                    Console.Clear();
                    RunMenu();
                    break;




            }

        }

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
            string inputCampground = CLIHelper.GetString("Select a campground (enter Q to quit)");

            if (inputCampground.ToUpper() == "Q")
            {
                Environment.Exit(0);
            }

            IList<Campground> campgrounds = campgroundDAO.ReadToListCampground(selectedParkId);
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
            numDays = inputEndDate.Subtract(inputStartDate);
            intDays = numDays.TotalDays;

            IList<Site> siteAvailList = siteDAO.ReadToListSite(selectedCampgroundId, inputStartDate, inputEndDate);
            Console.WriteLine("Results Matching Your Search Criteria");
            if (siteAvailList.Count == 0)
            {

                Console.WriteLine("No sites are available for your dates. Would you like to enter alternate dates? Y/N");
                string yesOrNo = "";
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
                    RunMenu();
                }
                else if (yesOrNo.ToUpper() == "Y")
                {
                    SearchReservations();
                }


            }
            else
            {

                Console.WriteLine($"Campground\tSiteNo.\tMax Occup.\tAccessible?\tRV Length\tUtilities Available?\tTotal Cost for {intDays} days");
                foreach (Site slot in siteAvailList)
                {
                    decimal campTotal = selectedCampgroundCost * (decimal)intDays;
                    Console.WriteLine(selectedCampgroundName + "\t" + slot.SiteId + "\t" + slot.MaxOccupancy + "\t" + slot.Accessible + "\t" + slot.MaxRvLength + "\t" + slot.Utilities + "\t" + campTotal);
                }

                Console.Write("Which site should be reserved? Enter site number or 0 to quit: ");

                // do error checking on input here    
                // make input correspond to the actual site
                inputSiteReserve = Convert.ToInt32(Console.ReadLine());
                if (inputSiteReserve == 0)
                {
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("What name should the reservation be made under?");
                    inputNameReserve = (Console.ReadLine());
                }

                int id = reservationDAO.AddReservation(inputNameReserve, inputSiteReserve, inputStartDate, inputEndDate);

                //IList<Reservation> reservationMade = reservationDAO.AddReservation(inputNameReserve, inputSiteReserve, inputStartDate, inputEndDate);

                if (id != 0)
                {
                    Console.WriteLine($"The reservation has been made, and the confirmation id is {id}");
                }
                else
                {
                    Console.WriteLine("Reservation unsuccessful.");
                    //option to go back
                }
            }

        }

    }
    

}
