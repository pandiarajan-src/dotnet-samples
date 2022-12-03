using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WeatherApp.ViewModal.ValueConverters
{
    public class Bool2RainConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isRaining = System.Convert.ToBoolean(value);
            if (isRaining) { return "Currently Raining"; }
            return "Currently Not Raining";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string isRaining = System.Convert.ToString(value) ?? "";
            if(isRaining.ToLower().Equals("currently raining")) { return true; }
            return false;
        }
    }
}
