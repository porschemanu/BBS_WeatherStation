namespace BBS_Wittlich.Weatherstation.Server.Pages
{
    public partial class Area51
    {
       
        
        protected override async Task OnInitializedAsync()
        {
            
            //await Task.Run(() => DataFetcher(query));
        }

        private async Task DataFetcher(Data.Query query)
        {   
            Data.QueryManager queryManager = new Data.QueryManager();
            Console.WriteLine($"Fetching Weather Data {query.Source} {query.UnitOfWork} {query.Timespan}");
            Data.Respone respone = queryManager.QueryAdministration(query);
            weatherData = respone.Result;
            Console.WriteLine($"Finished Fetching Weather Data: {weatherData.Length}");
        }

    }
}
