using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Model;
using System.Net.Http;
using Newtonsoft.Json;


namespace WeatherApp.ViewModal.Helpers
{
    public class AccuWeatherHelper
    {
        private static readonly string ACCU_BASE_URL = "http://dataservice.accuweather.com";
        private static readonly string API_KEY = "kTwJ7oWmjoRcJ3C3MR72zKALdeYhVA2a";
        private static readonly string LOCATION_URL_REF = "/locations/v1/cities/autocomplete?apikey={0}&q={1}";
        private static readonly string CONDITIONS_URL_REF = "/currentconditions/v1/{0}?apikey={1}";

        public static async Task<List<City>> GetListOfLocation(string search_loc)
        {
            var loc_list = new List<City>();
            try
            {
                string url = ACCU_BASE_URL + string.Format(LOCATION_URL_REF, API_KEY, search_loc);
                using HttpClient client = new HttpClient();
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                loc_list = JsonConvert.DeserializeObject<List<City>>(json);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
            return loc_list;
        }

        public static async Task<CurrentConditions> GetCurrentCondition(string loc_id)
        {
            CurrentConditions condition = new CurrentConditions();
            try
            {
                string url = ACCU_BASE_URL + string.Format(CONDITIONS_URL_REF, loc_id, API_KEY);
                using HttpClient client = new HttpClient();
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                condition = JsonConvert.DeserializeObject<List<CurrentConditions>>(json)?.FirstOrDefault() ?? new CurrentConditions();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
            return condition;
        }

    }
}
