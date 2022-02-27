using System.Collections.Generic;
using System.Linq;
using Microsoft.Win32;
using System.Diagnostics;
using System;
using System.Globalization;


namespace project1
{
    /// Statistic class that allows user to work with the data files

    class Statistics
    {
        // Properties
        internal Dictionary<string, CityInfo> cityDict = new Dictionary<string, CityInfo>();

        // Constructor
        public Statistics(string fileName, string fileType)
        {
            cityDict = DataModeler.ParseFile(fileName, fileType);
        }

        /// returns a list with cityinfo objects
        public List<CityInfo> DisplayCityInformation(string city)
        {
            string touppercity = allToUpper(city);
            List<CityInfo> cityArr = new List<CityInfo>();

            foreach (KeyValuePair<string, CityInfo> cty in cityDict)
            {
                if (cty.Value.CityName == touppercity)
                {
                    cityArr.Add(cty.Value);
                }
            }

            return cityArr;
        }

        // Display the city with the highest population within a city
        public CityInfo DisplayLargestPopulationCity(string province)
        {
            string toupperprovince = allToUpper(province);
            return cityDict.Where(city => city.Value.Province == toupperprovince).OrderByDescending(city => double.Parse(city.Value.Population)).First().Value;
        }

        // Display the city with the smallest population within a province
        public CityInfo DisplaySmallestPopulationCity(string province)
        {
            string toupperprovince = allToUpper(province);
            return cityDict.Where(city => city.Value.Province == toupperprovince).OrderBy(city => double.Parse(city.Value.Population)).First().Value;
        }


        //Compares the population in two given cities
        public (CityInfo, long, long) CompareCitiesPopulation(string city1, string city2)
        {
            string firstcity = allToUpper(city1);
            string secondcity = allToUpper(city2);
            long smallerPop;
            long biggerPop;

            CityInfo city;

            List<CityInfo> cityList = new List<CityInfo>();

            foreach (KeyValuePair<string, CityInfo> cty in cityDict)
            {
                if (cty.Value.CityName == firstcity)
                {
                    cityList.Add(cty.Value);
                }
                else if (cty.Value.CityName == secondcity)
                {
                    cityList.Add(cty.Value);
                }
            }

            if (double.Parse(cityList[0].Population) > double.Parse(cityList[1].Population))
            {
                city = cityList[0];
                smallerPop = long.Parse(cityList[1].Population);
                biggerPop = long.Parse(cityList[0].Population);
            }
            else
            {
                city = cityList[1];
                smallerPop = long.Parse(cityList[0].Population);
                biggerPop = long.Parse(cityList[1].Population);
            }

            return (city, smallerPop, biggerPop);
        }
        // starts a browser with google maps using a concatenated string using city + province
        public void ShowCityOnMap(string city, string province)
        {
            Process process = new Process();
            process.StartInfo.UseShellExecute = true;
            process.StartInfo.FileName = "https://www.google.com/maps/place/" + city + "," + province;
            process.Start();
        }

        // Return all province's populations
        public long DisplayProvincePopulation(string province)
        {
            string provinceupper = allToUpper(province);
            return cityDict.Where(city => city.Value.Province == provinceupper).Sum(city => long.Parse(city.Value.Population));
        }

        // Method that returns a list of all cities in a province
        public List<CityInfo> DisplayProvinceCities(string province)
        {
            string provinceupper = allToUpper(province);
            return cityDict.Values.Where(city => city.Province == provinceupper).ToList();
        }

        // Method that returns a city + its city amount dictionary sorted by #of cities
        public Dictionary<string, long> RankProvincesByPopulation()
        {
            return (from city in cityDict.Values group city by new { city.Province } into province select new { province.Key.Province, Population = (long)province.Sum(prov => long.Parse(prov.Population)) }).OrderBy(prov => prov.Population).ToDictionary(x => x.Province, y => y.Population);
        }


        // Method that returns a city + its city amount dictionary sorted by #of cities
        public Dictionary<string, int> RankProvincesByCities()
        {
            return (from city in cityDict.Values group city by new { city.Province } into province select new { province.Key.Province, Count = province.Count() }).OrderBy(prov => prov.Count).ToDictionary(x => x.Province, y => y.Count);
        }

        //method that converts passed string to uppercase
        public string allToUpper(string str)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
        }


        //Method that returns a string of a passed city object
        public string toString(CityInfo cty)
        {
            return "name: " + cty.CityName + "\n" +
                "id: " + cty.CityID + "\n" +
                "ascii: " + cty.CityAscii + "\n" +
                "latitude: " + cty.Latitude + "\n" +
                "longitude: " + cty.Longitude + "\n" +
                "population: " + cty.Population + "\n" +
                "province: " + cty.Province;
        }

        public string getCityName(CityInfo cty)
        {
            return "name: " + cty.CityName;
        }
    }   // end Statistics class

}   // end Statistics class

