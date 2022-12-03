using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Model
{
    public class Area
    {
        public string ID { get; set; }
        public string LocalizedName { get; set; }
        public Area() { ID = ""; LocalizedName = ""; }
    }

    public class City
    {
        public City()
        {
            Version = 0; Key = "";Type = ""; Rank = 0; LocalizedName = "";
            Country = new Area();
            AdministrativeArea = new Area();
        }
        public int Version { get; set; }
        public string Key { get; set; }
        public string Type { get; set; }
        public int Rank { get; set; }
        public string LocalizedName { get; set; }
        public Area Country { get; set; }
        public Area AdministrativeArea { get; set; }

    }
}
