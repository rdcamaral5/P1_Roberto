using Microsoft.Win32;
using System;
using System.IO;

// Project 1
// Author: Roberto Amaral
// Date: 2022-02-25
// v1.3

namespace project1
{
    public class Program
    {
        static void Main(string[] args)
        {
            // stats object
            Statistics stats;

            //read xml interface
            Console.WriteLine("Please select a file type to open: ");
            Console.WriteLine("a) xml");
            Console.WriteLine("b) json");
            Console.WriteLine("c) csv");
            string filename = Console.ReadLine();
            Console.WriteLine("You entered " + filename.ToString() + "");
            //input failsafe
            while (filename != "a" && filename != "b" && filename != "c")
            {
                Console.WriteLine("Please enter a valid option");
                Console.WriteLine("a) xml");
                Console.WriteLine("b) json");
                Console.WriteLine("c) csv");
                filename = Console.ReadLine();
            }

            //directory var
            string[] filedirectory = System.IO.Directory.GetFiles("./Data/");

            //check selected file and populate the stats object with the file of that type
            switch (filename)
            {
                //xml
                case "a":
                    stats = new Statistics(filedirectory[1], "xml");
                    Console.WriteLine("You loaded the file using xml...");
                    break;
                //xml json
                case "b":
                    stats = new Statistics(filedirectory[0], "json");
                    Console.WriteLine("You loaded the file using json...");
                    break;
                //xml csv
                case "c":
                    stats = new Statistics(filedirectory[2], "csv");
                    Console.WriteLine("You loaded the file using csv...");
                    break;
                default:
                    stats = null;
                    Console.WriteLine("Something went wrong with the switch case");
                    break;
            }

            //option
            string option = "";

            //check user has not chosen to exit the app
            while (option != "z")
            {
                Console.WriteLine("\nSelect your next option by entering the corresponding letter: ");
                Console.WriteLine("a) display city information");
                Console.WriteLine("b) Display largest population city by province");
                Console.WriteLine("c) Display smalest population city by province");
                Console.WriteLine("d) Compare the population of two cities");
                Console.WriteLine("e) Show city on map");
                Console.WriteLine("f) Show province population");
                Console.WriteLine("g) List cities in province");
                Console.WriteLine("h) Rank provinces by population");
                Console.WriteLine("i) Rank provinces by city count");
                Console.WriteLine("z) Exit");
                option = Console.ReadLine();

                //input validation for the options menu
                while (option != "a" && option != "b" && option != "c" && option != "d" && option != "e" && option != "f" && option != "g" && option != "h" && option != "i" && option != "z")
                {
                    Console.WriteLine("Please select a valid option by entering the corresponding letter: ");
                    Console.WriteLine("a) Display city information");
                    Console.WriteLine("b) Display largest population city by province");
                    Console.WriteLine("c) Display smalest population city by province");
                    Console.WriteLine("d) Compare the population of two cities");
                    Console.WriteLine("e) Show city on map");
                    Console.WriteLine("f) Show province population");
                    Console.WriteLine("g) List cities in province");
                    Console.WriteLine("h) Rank provinces by population");
                    Console.WriteLine("i) Rank provinces by city count");
                    Console.WriteLine("z) Exit");
                }

                // option A, DisplayCityInformation
                if (option.ToLower() == "a")
                {
                    Console.WriteLine("enter city name: ");
                    string ctyname = Console.ReadLine();
                    List<CityInfo> ctyinf = stats.DisplayCityInformation(ctyname);

                    Console.WriteLine("We found " + ctyinf.Count + " city/cities with that name: ");
                    //print
                    for (int i = 0; i < ctyinf.Count; i++)
                    {
                        Console.WriteLine("--------------------------");
                        Console.WriteLine(stats.toString(ctyinf[i]));
                        Console.WriteLine("--------------------------");
                    }
                }

                // option B and C, Display Largest Population City and Display Smallest Population City
                else if (option.ToLower() == "b" || option.ToLower() == "c")
                {
                    Console.WriteLine("enter province name: ");
                    string provname = Console.ReadLine();

                    //check input
                    if (option == "b")
                    {
                        CityInfo highestpop = stats.DisplayLargestPopulationCity(provname);
                        //print
                        Console.WriteLine(stats.toString(highestpop));
                    }
                    else
                    {
                        CityInfo lowestpop = stats.DisplaySmallestPopulationCity(provname);
                        //print
                        Console.WriteLine(stats.toString(lowestpop));
                    }

                }
                // option D, compare cities POP
                else if (option.ToLower() == "d")
                {
                    Console.WriteLine("enter the first city name: ");
                    string cty1 = Console.ReadLine();
                    Console.WriteLine("enter the second city name: ");
                    string cty2 = Console.ReadLine();

                    CityInfo cty;
                    long smallPop;
                    long bigPop;

                    (cty, smallPop, bigPop) = stats.CompareCitiesPopulation(cty1, cty2);
                    //print
                    Console.WriteLine("Since " + bigPop + " > " + smallPop + ", the bigger city is, ");
                    Console.WriteLine(stats.toString(cty));

                }
                //option e, show city on map
                else if (option.ToLower() == "e")
                {
                    Console.WriteLine("enter the city name: ");
                    string cty = Console.ReadLine();
                    Console.WriteLine("enter the province name: ");
                    string prov = Console.ReadLine();

                    stats.ShowCityOnMap(cty, prov);
                    //print
                    Console.WriteLine("Web browser successfully opened! ");
                }
                //option f, display province population
                else if (option.ToLower() == "f")
                {
                    Console.WriteLine("enter the province name: ");
                    string prov = Console.ReadLine();

                    long totalpop;
                    totalpop = stats.DisplayProvincePopulation(prov);
                    //print
                    Console.WriteLine("The population of " + prov + " on file is " + totalpop);
                }
                //option g, display all cities in aprovince
                else if (option.ToLower() == "g")
                {
                    Console.WriteLine("enter the province name: ");
                    string prov = Console.ReadLine();

                    List<CityInfo> citylist;
                    int count = 0;
                    citylist = stats.DisplayProvinceCities(prov);

                    Console.WriteLine("We have record of " + citylist.Count + " cities in " + prov + ", they are:");

                    //print
                    foreach (var item in citylist)
                    {
                        Console.WriteLine("-----------------------");
                        Console.WriteLine(stats.toString(citylist[count]));
                        Console.WriteLine("-----------------------");
                        count++;
                    }

                }
                //option h, list provinces by population, highest to lowest
                else if (option.ToLower() == "h")
                {

                    Dictionary<string, long> orderedCities;
                    orderedCities = stats.RankProvincesByPopulation();

                    int count = 1;
                    //print
                    foreach (KeyValuePair<string, long> kp in orderedCities.Reverse())
                    {
                        Console.WriteLine("Ranked: " + count);
                        Console.WriteLine(kp.Key + " with a population of " + kp.Value);
                        Console.WriteLine("-----------------------");
                        count++;
                    }

                }
                //option u, list provinces by amount of cities
                else if (option.ToLower() == "i")
                {

                    Dictionary<string, int> orderedCities;
                    orderedCities = stats.RankProvincesByCities();

                    int count = 1;
                    //print
                    foreach (KeyValuePair<string, int> kp in orderedCities.Reverse())
                    {
                        Console.WriteLine("Ranked: " + count);
                        Console.WriteLine(kp.Key + " with " + kp.Value + " cities");
                        Console.WriteLine("-----------------------");
                        count++;
                    }
                }
            }
        }
    }
}