namespace BBS_Wittlich.Weatherstation.Server.Pages
{
    public partial class Area51
    {
        Weatherstation.Data.Models.WeatherEntry[] weatherData;

        protected override async Task OnInitializedAsync()
        {
            Task.Run(() => DataFetcher());
        }

        private async void DataFetcher()
        {
            Console.WriteLine("Fetching Weather Data");
            Weatherstation.Data.Repositories.SQLClientRepo dataRepo = new();
            weatherData = dataRepo.GetAllWeatherEntries("temp");
            Array. weatherData[5]
            await Task.Delay(10000);
            DataFetcher();
        }

    }
}
