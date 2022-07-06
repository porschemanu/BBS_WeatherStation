using BBS_Weatherstation_SeriesA.Data.Repositories;
namespace BBS_Weatherstation_SeriesA.Data.Startup
{
    public class Startup
    {
        public static async Task<Task> StartupProcedure()
        {
            TableExists();
            await test.CleanData();
            await test.FillCache();
            Task.Run(() => SystemProcedure());
            return Task.CompletedTask;
        }

        public static async Task SystemProcedure()
        {
            await test.CleanData();
            test.FillCache();

            await Task.Delay(600000);
            SystemProcedure();
        }

        private static void TableExists()
        {

        }
    }
}
