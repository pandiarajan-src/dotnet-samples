namespace async_retry_onlyone
{
    public partial class Form1 : Form
    {
        private const string _base_url = "https://localhost:7015/api";

        private HttpClient _httpClient = new();
        //private CancellationTokenSource _cts;
        public Form1()
        {
            InitializeComponent();
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            progressBar.Visible = true;
            //_cts = new CancellationTokenSource();

            try
            {
                var response = await Retry<string>(ProcessGreetings);
                Console.WriteLine(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Retry operation has failed");
                Console.WriteLine(ex.Message);
            }


            progressBar.Visible = false;
        }

        private async Task<string> ProcessGreetings()
        {
            using (var response = await _httpClient.GetAsync($"{_base_url}/greetings/{txtInput.Text}"))
            {
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content);
                return content;
            }
        }

        private async Task<T> Retry<T>(Func<Task<T>> method, int retry_count = 3, int wait_time = 3)
        {
            for (int i = 0; i < retry_count-1; i++)
            {
                try
                {
                    await Task.Delay(TimeSpan.FromSeconds(2));
                    return await method();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    await Task.Delay(TimeSpan.FromSeconds(wait_time));
                }
                
            }
            return await method();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //_cts.Cancel();
        }
    }
}