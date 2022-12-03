using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WeatherApp.ViewModal.Command
{
    public class SearchCommand : ICommand
    {
        public SearchCommand(WeatherVM weatherVM)
        {
            this.weatherVM = weatherVM;
        }

        public WeatherVM weatherVM { get; set; }

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter)
        {
            if (parameter == null)
                return false;
            string? query_parameter = parameter as string;
            if (string.IsNullOrWhiteSpace(query_parameter))
                return false;
            return true;
        }

        public void Execute(object? parameter)
        {
            weatherVM.MakeQuery();
        }
    }
}
