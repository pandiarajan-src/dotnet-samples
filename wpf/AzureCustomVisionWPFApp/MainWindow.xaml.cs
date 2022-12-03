using Microsoft.VisualBasic.FileIO;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json.Serialization;
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

namespace AzureCustomVisionWPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SelectImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            openFileDialog.Title = "Select the Image to get predictions";
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "Image Files (*.jpg; *.png) | *.jpg; *.png; *.jpeg | All Files(*.*) | *.*";
            if (openFileDialog.ShowDialog() == true)
            {
                SelectedImage.Source = new BitmapImage(new Uri(openFileDialog.FileName));
                PredictImageContent(openFileDialog.FileName);
            }
        }

        private async void PredictImageContent(string filename)
        {
            string api_uri = "https://eastus.api.cognitive.microsoft.com/customvision/v3.0/Prediction/56d7ce7f-9162-4c22-ae50-f8d8c1f025aa/classify/iterations/LandmarkPrediction/image";
            string content_type = "application/octet-stream";
            string prediction_key = "2f185df2d09e401bbc6da97194a8866e";

            try
            {
                var file_content = File.ReadAllBytes(filename);

                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("Prediction-Key", prediction_key);
                    using (var content = new ByteArrayContent(file_content))
                    {
                        content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(content_type);
                        var response = await httpClient.PostAsync(api_uri, content);
                        response.EnsureSuccessStatusCode();

                        var response_str = await response.Content.ReadAsStringAsync();
                        IList<Prediction> predictions = (JsonConvert.DeserializeObject<CustomVision>(response_str)).predictions;
                        PredictionList.ItemsSource = predictions;
                    }
                }
            }
            catch(HttpRequestException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
