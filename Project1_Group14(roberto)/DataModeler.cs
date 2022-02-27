using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace project1
{

    class DataModeler
    {
        //properties
        private static Dictionary<string, CityInfo> cityDict;
        private delegate void Delegate(string file);

        //methods

        //parse file that uses a delegate
        public static Dictionary<string, CityInfo> ParseFile(string file, string filetype)
        {
            cityDict = new Dictionary<string, CityInfo>();
            Delegate del;
            // check input type
            if (filetype == "xml")
            {
                del = ParseXML;
                del(file);
                return cityDict;
            }
            else if (filetype == "json")
            {
                del = ParseJSON;
                del(file);
                return cityDict;
            }
            else if (filetype == "csv")
            {
                del = ParseCSV;
                del(file);
                return cityDict;
            }
            else
            {
                return cityDict;
            }
        }

        //parse xml file, uses XmlDocument
        private static void ParseXML(string file)
        {
            XmlDocument document = new XmlDocument();
            document.Load(file);

            //loop the xml document
            foreach (XmlNode node in document.DocumentElement.ChildNodes)
            {
                string id = node.ChildNodes[8]?.InnerText;
                string name = node.ChildNodes[0]?.InnerText;
                string asci = node.ChildNodes[1]?.InnerText;
                string population = node.ChildNodes[7]?.InnerText;
                string province = node.ChildNodes[5]?.InnerText;
                string lat = node.ChildNodes[2]?.InnerText;
                string lng = node.ChildNodes[3]?.InnerText;

                CityInfo cityInfo = new CityInfo(id, name, asci, population, province, lat, lng);

                // add keys to dictionary
                string key = $"{name}, {province}";
                if (!cityDict.ContainsKey(key))
                {
                    if (name != string.Empty)
                        cityDict.Add(key, cityInfo);
                }
            }
        }

        //parse xml file, uses StreamReader and JArray
        private static void ParseJSON(string file)
        {
            using StreamReader r = new StreamReader(file);
            string json = r.ReadToEnd();
            JArray items = JsonConvert.DeserializeObject<JArray>(json);

            //populates dictionary
            foreach (var item in items.Children())
            {
                string Id = item.Value<string>("id");
                string name = item.Value<string>("city");
                string asci = item.Value<string>("city_ascii");
                string population = item.Value<string>("population");
                string province = item.Value<string>("admin_name");
                string lat = item.Value<string>("lat");
                string lng = item.Value<string>("lng");

                CityInfo cityInfo = new CityInfo(Id, name, asci, population, province, lat, lng);

                // add to dictionary
                string key = $"{name}, {province}";
                if (!cityDict.ContainsKey(key))
                {
                    if (name != string.Empty)
                        cityDict.Add(key, cityInfo);
                }
            }
        }

        //parse csv file, uses File
        private static void ParseCSV(string file)
        {
            List<string> cityData = new List<string>(File.ReadAllLines(file));
            // remove headers
            cityData.RemoveAt(0);

            //populates dictionary
            foreach (var c in cityData)
            {
                string[] city = c.Split(',');

                string Id = city[8];
                string name = city[0];
                string asci = city[1];
                string population = city[7];
                string province = city[5];
                string lat = city[2];
                string lng = city[3];
                string capital = city[6];

                CityInfo cityInfo = new CityInfo(Id, name, asci, population, province, lat, lng);

                // add to dictionary
                string key = $"{name}, {province}";
                if (!cityDict.ContainsKey(key))
                {
                    if (name != string.Empty)
                        cityDict.Add(key, cityInfo);
                }
            }
        }


    }

}