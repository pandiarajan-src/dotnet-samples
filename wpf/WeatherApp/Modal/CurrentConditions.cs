using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Model
{
    public class UnitData
    {
        public UnitData() { Value = "";  Unit = ""; UnitType = 0; }
        public string Value { get; set; }
        public string Unit { get; set; }
        public int UnitType { get; set; }
    }

    public class Temperature
    {
       public Temperature()
        {
            Metric = new UnitData();
            Imperial = new UnitData();
        }
        public UnitData Metric { get; set; }
        public UnitData Imperial { get; set; }
    }

    public class CurrentConditions
    {
        public CurrentConditions()
        {
            EpochTime = 0; WeatherText = "";HasPrecipitation = false; PrecipitationType = "";
            IsDayTime = false; MobileLink = "";Link = "";
            Temperature = new Temperature();
         }
        public DateTime LocalObservationDateTime { get; set; }
        public int EpochTime { get; set; }
        public string WeatherText { get; set; }
        public int WeatherIcon { get; set; }
        public bool HasPrecipitation { get; set; }
        public string PrecipitationType { get; set; }
        public bool IsDayTime { get; set; }
        public Temperature Temperature { get; set; }
        public string MobileLink { get; set; }
        public string Link { get; set; }
    }
}
