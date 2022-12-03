using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading;

namespace async_winform_sample
{
    public partial class Async_Await_Sample : Form
    {
        private const string _base_url = "https://localhost:7015/api";
        private HttpClient _httpClient;
        CancellationTokenSource? _cancellationTokenSource;
        public Async_Await_Sample()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
        }

        private async void Start_Click(object sender, EventArgs e)
        {
            _cancellationTokenSource = new();
            
            //Incase if you want to cancel the thread with the specific time delay.
            //_cancellationTokenSource.CancelAfter(TimeSpan.FromSeconds(5));

            ProgressGIF.Visible = true;
            pgCards.Visible = true;

            var progress_report = new Progress<int>(ReportProgressofCardCompletion);


            Stopwatch stopwatch = new Stopwatch();

            // This commented section is just to test the wait method
            // await Wait();
            // MessageBox.Show("Wait is over!!!");
            try
            {
                //var result = await GetGreetingsAPIResult(txtInput.Text);
                //MessageBox.Show(result);

                stopwatch.Start();
                List<string> cards = await GetListOfCards(300, _cancellationTokenSource.Token);

                await ProcessGreetingsCards(cards, progress_report, _cancellationTokenSource.Token);
                MessageBox.Show($"Time taken to process all Greetings cards {stopwatch.ElapsedMilliseconds / 1000} seconds");
            }
            catch(TaskCanceledException ex)
            {
                MessageBox.Show("Task was cancelled " + ex.ToString());
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                _cancellationTokenSource.Dispose();
            }
            ProgressGIF.Visible = false;
            pgCards.Visible= false;
            pgCards.Value = 0;
            _cancellationTokenSource = null;
        }

        private async Task<List<string>> GetListOfCards(int cardsCount, CancellationToken token = default)
        {
            return await Task.Run(() =>
            {
                List<string> cards = new();
                for (int i = 0; i < cardsCount; i++)
                {
                    cards.Add(i.ToString().PadLeft(16, '0'));
                    token.ThrowIfCancellationRequested();
                }

                //This is the simplest method instead of for loop.
                //List<string> cards = Enumerable.Range(0, cardsCount).Select(x => x.ToString().PadLeft(10, '0')).ToList();
                return cards;
            });
        }

        private async Task ProcessGreetingsCards(List<string> cards, IProgress<int> progress, CancellationToken token = default)
        {
            var tasks = new List<Task<HttpResponseMessage>>();

            //int taskResolved = 0;

            SemaphoreSlim semaphore = new(10);

            tasks = cards.Select( async card =>
            {
                var Json = JsonConvert.SerializeObject(card);
                var content = new StringContent(Json, Encoding.UTF8, "application/json");
                await semaphore.WaitAsync();
                try
                {
                    var internalTask = await _httpClient.PostAsync($"{_base_url}/cards", content, token);

                    //This can also be replaced with Tasks.WhenAny in the below code block
                    //REFER WhenAny() below
                    //if (progress != null)
                    //{
                    //    taskResolved++;
                    //    int percentage = Convert.ToInt32(Math.Round( ( ((double)taskResolved/cards.Count) * 100), 0));
                    //    progress.Report(percentage);
                    //}
                    return internalTask;
                }
                finally
                {

                    semaphore.Release();
                }
            }).ToList();

            var responseTasks = Task.WhenAll(tasks);

            if (progress != null)
            {
                while(await Task.WhenAny(responseTasks, Task.Delay(TimeSpan.FromSeconds(5))) != responseTasks)
                {
                    var _completedTasks = tasks.Where(x => x.IsCompleted).Count();
                    int percentage = Convert.ToInt32(Math.Round((((double)_completedTasks / cards.Count) * 100), 0));
                    progress.Report(percentage);
                }
            }

            var responses = await responseTasks;

            foreach (var response in responses)
            {
                var content = await response.Content.ReadAsStringAsync();
                CardResponse resp = JsonConvert.DeserializeObject<CardResponse>(content) ?? new CardResponse("xxx", false);
                if(!(resp.Approved))
                {
                    Console.WriteLine($"{resp.Card} is rejected");
                }
            }
        }

        private void ReportProgressofCardCompletion(int percentage)
        {
            pgCards.Value = percentage;
        }

        //private async Task Wait()
        //{
        //    await Task.Delay(TimeSpan.FromSeconds(5));
        //}
        private async Task<string> GetGreetingsAPIResult(string name)
        {
            using var responseMessage = await _httpClient.GetAsync($"{_base_url}/greetings/{name}");
            responseMessage.EnsureSuccessStatusCode();
            var result = await responseMessage.Content.ReadAsStringAsync();
            return result;
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            _cancellationTokenSource?.Cancel();
        }
    }
}