using Delta.Model;
using SQLite;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace Delta.Services
{
    public class StepsService
    {
        const float Steplength = 77;
        const int Targetsteps = 10000;

        SQLiteAsyncConnection Database;

        async Task Init()
        {
            if (Database is not null)
            return;

            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            await Database.CreateTableAsync<Steps>();
        }

        public async Task<List<Steps>> GetStepsAsync()
        {
            await Init();
            return await Database.Table<Steps>().ToListAsync();
        }

        public async Task<List<Steps>> GetStepsByMonthAsync(string month)
        {
            await Init();
            string searchString = "." + month + ".";
            return await Database.Table<Steps>().Where(i => i.Date.Contains(searchString)).OrderBy(i => i.Date).ToListAsync();
        }

        public async Task<Steps> GetStepsByDateAsync(string Date)
        {
            await Init();

            var datesteps = await Database.Table<Steps>().Where(i => i.Date == Date).FirstOrDefaultAsync();
            //return await Database.Table<Steps>().Where(i => i.Date == Date).FirstOrDefaultAsync();
            return datesteps;
        }

        public async Task<Steps> GetStepsAsync(int id)
        {
            await Init();
            return await Database.Table<Steps>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        //Methode um zufaellig generierte Daten in die Datenbank zu speichern.
        public async Task SaveItemAsync(int totalsteps, string date)
        {
            await Init();

            float wd = totalsteps * Steplength / 100000;
            int stepsleft = Targetsteps - totalsteps;

            if (stepsleft <= 0)
                stepsleft = 0;

            var steps = new Steps
            {
                Totalsteps = totalsteps,
                Stepsleft = stepsleft,
                Walkdistance = wd.ToString("0.00"),
                Date = date
            };

            var id = await Database.InsertAsync(steps);
        }

        //Zu Testzwecken die Tabelle löschen.
        public async Task DropTable()
        {
            await Init();
            await Database.DropTableAsync<Steps>();
        }
    }
}
