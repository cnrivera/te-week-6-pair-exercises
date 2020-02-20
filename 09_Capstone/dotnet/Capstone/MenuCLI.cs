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

            RunMenu();
        }

        public void RunMenu()
        {

            PrintMenu();
            
            string input = Console.ReadLine().ToUpper();
            int holdParkId = 0;
            {
                IList<Park> parkSelection = parkDAO.ViewAvailableParks();
                Console.Clear();
                for (int i = 0; i < parkSelection.Count; i++)

                    if (input == (i + 1).ToString())
                    {
                        holdParkId = parkSelection[i].ParkId;
                        Console.WriteLine("Park Information Screen");
                        Console.WriteLine(parkSelection[i].Name + " National Park");
                        Console.WriteLine("Location: " + parkSelection[i].Location);
                        Console.WriteLine("Established: " +parkSelection[i].EstablishDate);
                        Console.WriteLine("Area: " + parkSelection[i].Area + " sq km");
                        Console.WriteLine("Annual Visitors: " + parkSelection[i].Visitors);
                        Console.WriteLine();
                        Console.WriteLine(parkSelection[i].Description);
                        break;
                    }
                    if (input == "Q")
                    {
                        Environment.Exit(0);
                    }
                    else
                    {
                        Console.WriteLine("Invalid selection. Please choose from the above options.");
                        Console.WriteLine();
                      
                    }

            }
            PrintCampgroundMenu();
            RunCampgroundMenu();


        }


        public void PrintMenu()
        {
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

        public void ViewCampgrounds(int holdParkId)
        {
            IList<Campground> campInfo = campgroundDAO.ReadToListCampground();
            Console.WriteLine("Name Open Close Daily Fee");
            for (int i = 0; i < campInfo.Count; i++)
            {
                if (campInfo[i].CampgroundId == holdParkId)
                {
                    Console.WriteLine(i+1 + campInfo[i].Name + " " + campInfo[i].OpenFrom + " " + campInfo[i].OpenTo + " " + campInfo[i].DailyFee);

                }
                
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
                    ViewCampgrounds(int holdParkId);
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
