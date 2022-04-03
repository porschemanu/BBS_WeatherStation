namespace BBS_Wittlich.Weatherstation.Server.Pages
{
    public partial class Index
    {
        Weatherstation.Data.Models.WeatherEntry[] weatherData;

        protected override async Task OnInitializedAsync()
        {
            Task.Run(() => DataFetcher());
        }

        private async void DataFetcher()
        {
            Console.WriteLine("Fetching Data");
            Weatherstation.Data.Repositories.SQLClientRepo dataRepo = new();
            weatherData = dataRepo.GetAllWeatherEntries("humidity");
            await Task.Delay(10000);
            DataFetcher();
        }
    }
}
