using BBS_Weatherstation_SeriesA.Data.Repositories;
namespace BBS_Weatherstation_SeriesA.Data.Startup
{
    public class Startup
    {
        public static async Task<Task> StartupProcedure()
        {
            TableExists();
            await DataRepo.CleanData();
            await DataRepo.FillCache();
            Task.Run(() => SystemProcedure());
            return Task.CompletedTask;
        }

        public static async Task SystemProcedure()
        {
            await DataRepo.CleanData();
            DataRepo.FillCache();

            await Task.Delay(600000);
            SystemProcedure();
        }

        private static void TableExists()
        {

        }
    }
}
