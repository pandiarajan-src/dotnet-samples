using CalcLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace wpf_calc
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ICalc? calc;
        private double lastNumber = 0.0;
        private string? operand = "";
        public MainWindow()
        {
            InitializeComponent();
            btnAC.Click += BtnAC_Click; ;
            btnSigns.Click += BtnSigns_Click; ;
            btnPercentage.Click += BtnPercentage_Click;
            btnDot.Click += BtnDot_Click;

            ConfigureServices();

        }

        private void ConfigureServices()
        {
            calc = new Calc();
            calc?.Init();
        }

        private void BtnDot_Click(object sender, RoutedEventArgs e)
        {
            NumberButton_Click(sender, e);
        }

        private void BtnPercentage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender == btnPercentage)
                {
                    lblResult.Content = Convert.ToString(calc?.OwnPercentage(Convert.ToDouble(lblResult.Content)));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Exception occured {ex.Message}");
            }
        }

        private void BtnSigns_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender == btnSigns)
                {
                    lblResult.Content = Convert.ToString(calc?.Negate(Convert.ToDouble(lblResult.Content)));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Exception occured {ex.Message}");
            }

        }

        private void BtnAC_Click(object sender, RoutedEventArgs e)
        {
            lblResult.Content = Convert.ToString(calc?.Init());
        }

        private void OperatorButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string? buttonPressed = ((Button)sender).Content.ToString();
                lastNumber = Convert.ToDouble(lblResult.Content);
                lblResult.Content = "0";
                operand = buttonPressed;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error in Operation");
                MessageBox.Show(ex.Message);
            }

        }

        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            string? buttonPressed = ((Button)sender).Content.ToString();
            if (lblResult.Content.Equals("0"))
            {
                lblResult.Content = $"{buttonPressed}";
            }
            else
            {
                lblResult.Content = $"{lblResult.Content}{buttonPressed}";
            }

        }

        private void btnEqual_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double newNumber = Convert.ToDouble(lblResult.Content);
                switch (operand)
                {
                    case "+":
                        lblResult.Content = Convert.ToString(calc?.Add(lastNumber, newNumber));
                        break;
                    case "-":
                        lblResult.Content = Convert.ToString(calc?.Subtract(lastNumber, newNumber));
                        break;
                    case "X":
                        lblResult.Content = Convert.ToString(calc?.Multiply(lastNumber, newNumber));
                        break;
                    case "/":
                        lblResult.Content = Convert.ToString(calc?.Divide(lastNumber, newNumber));
                        break;
                    default:
                        MessageBox.Show("Invalid Operation");
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in Operation");
                MessageBox.Show(ex.Message);
            }

        }
    }
}
