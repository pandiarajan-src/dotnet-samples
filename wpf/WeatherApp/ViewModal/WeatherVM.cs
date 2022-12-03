using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WeatherApp.Model;
using WeatherApp.ViewModal.Command;
using WeatherApp.ViewModal.Helpers;

namespace WeatherApp.ViewModal
{
    public class WeatherVM : INotifyPropertyChanged
    {
        public WeatherVM() 
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
            {
                CurrentConditions = new CurrentConditions
                {
                    WeatherText = "Sunny",
                    Temperature = new Temperature
                    {
                        Metric = new UnitData
                        {
                            Value = "22",
                            Unit = "C"
                        }
                    }
                };
                SelectedCity = new City
                {
                    LocalizedName = "New york"
                };
            }
            query = "";
            SearchCommand = new SearchCommand(this);
            Cities = new ObservableCollection<City>();
        }

        public ObservableCollection<City> Cities { get; set; }

        private string query;

        public string Query
        {
            get { return query; }
            set 
            { 
                query = value; 
                OnPropertyChanged("Query");
            }
        }

        private CurrentConditions currentConditions;

        public CurrentConditions CurrentConditions
        {
            get { return currentConditions; }
            set 
            {
                currentConditions = value;
                OnPropertyChanged("CurrentConditions");

            }
        }

        private City selectedCity;

        public City SelectedCity
        {
            get { return selectedCity; }
            set 
            { 
                selectedCity = value;
                OnPropertyChanged("SelectedCity");
                GetCurrentConditions();
            }
        }

        public SearchCommand SearchCommand { get; set; }



        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async void MakeQuery()
        {
            var cities = await AccuWeatherHelper.GetListOfLocation(Query);

            Cities.Clear();
            foreach (City city in cities)
            {
                Cities.Add(city);
            }
        }

        public async void GetCurrentConditions()
        {
            Query = String.Empty;
            CurrentConditions = await AccuWeatherHelper.GetCurrentCondition(SelectedCity.Key);
        }

    }
}
