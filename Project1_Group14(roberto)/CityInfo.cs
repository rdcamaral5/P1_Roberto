namespace project1
{

    class CityInfo
    {
        //properties 
        public string CityID { get; set; }
        public string CityName { get; set; }
        public string CityAscii { get; set; }
        public string Population { get; set; }
        public string Province { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        //methods
        public string getProvince()
        {
            return this.Province;
        }
        public string getPopulation()
        {
            return this.Population;
        }
        public string getLocation()
        {
            return this.Latitude + " " + this.Longitude;
        }

        //city info object constructor
        public CityInfo(string id, string name, string ascii, string pop, string prov, string lati, string longi)
        {
            CityID = id;
            CityName = name;
            CityAscii = ascii;
            Population = pop;
            Province = prov;
            Latitude = lati;
            Longitude = longi;
        }

    }

}