using Delta.Model;
using System.Net.Http.Json;

namespace Delta.Services
{
    public class DrugService
    {
        //HttpClient httpClient;
        public DrugService()
        {
            //httpClient = new HttpClient();
        }

        List<Drug> drugList = new List<Drug>();

        //Online Variante
        //public async Task<List<Drug>> GetDrugs()
        //{
        //    if(drugList?.Count > 0)
        //        return drugList;

        //    var url = "https://raw.githubusercontent.com/mh1985/D_Data/main/drugdata.json";

        //    var response = await httpClient.GetAsync(url);

        //    if(response.IsSuccessStatusCode)
        //    {
        //        drugList = await response.Content.ReadFromJsonAsync<List<Drug>>();
        //    }

        //    return drugList;
        //}

        //Offline-Variante
        public async Task<List<Drug>> GetDrugs()
        {
            if (drugList?.Count > 0)
                return drugList;

#if WINDOWS
            //Öffnet lokale Datei auf der Festplatte (nur Windows)
            using (StreamReader r = new StreamReader(@"C:\delta\drugdata.json"))
            {
                string contents = await r.ReadToEndAsync();
                drugList = JsonSerializer.Deserialize<List<Drug>>(contents);
            }
#else
            //Prüft ob bereits eine "drugdata.json"-Datei im App-Verzeichnis vorhanden ist,
            //falls nicht wird CopyData aufgerufen
            if (!(File.Exists(System.IO.Path.Combine(FileSystem.Current.AppDataDirectory, "drugdata.json"))))
            {
                await this.CopyData();
            }

            /*
             *  Falls die Datei bereits erstellt wurde, wird diese gelesen, eine Drug-Liste erstellt und
             *  zurück gegeben.
             */
            var stream = System.IO.Path.Combine(FileSystem.AppDataDirectory, "drugdata.json");
            using var reader = new StreamReader(stream);
            var contents = await reader.ReadToEndAsync();

            drugList = JsonSerializer.Deserialize<List<Drug>>(contents);
#endif
            return drugList;
        }

        public async Task AddDrug(string name, string dose, string form, string frequency, string frequency2)
        {
            var drug = new Drug
            {
                Name = name,
                Dose = dose,
                Form = form,
                Frequency = frequency,
                Frequency2 = frequency2
            };

            drugList.Add(drug);

#if WINDOWS
            string targetFile = Path.Combine(@"C:\delta\drugdata.json");
            //Löschen der Datei damit keine Reste bleiben, falls die neue Liste kürzer ist als zu Beginn.
            System.IO.File.Delete(targetFile);
            using FileStream outputStream = System.IO.File.OpenWrite(targetFile);
            using StreamWriter sw = new StreamWriter(outputStream);
            await sw.WriteAsync(JsonSerializer.Serialize(drugList));

#else
            string targetFile = System.IO.Path.Combine(FileSystem.Current.AppDataDirectory, "drugdata.json");
            //Löschen der Datei damit keine Reste bleiben, falls die neue Liste kürzer ist als zu Beginn.
            System.IO.File.Delete(targetFile);
            using FileStream outputStream = System.IO.File.OpenWrite(targetFile);
            using StreamWriter streamWriter = new StreamWriter(outputStream);
            await streamWriter.WriteAsync(JsonSerializer.Serialize(drugList));
#endif
            return;
        }

        /* 
         *  Diese Methode wird aufgerufen, falls keine drugdata.json Datei im App-Verzeichnis.
         *  Es wird die Musterdatei drugdata.json aus dem App-Package geöffnet, eine Drug-Liste erstellt
         *  und eine "drugdata.json"-Datei im App-Verzeichnis erstellt.
         */
        public async Task CopyData()
        {
            //Lesen der Musterdatei aus dem App-Package
            using var stream = await FileSystem.OpenAppPackageFileAsync("drugdata.json");
            using var reader = new StreamReader(stream);
            var contents = await reader.ReadToEndAsync();

            drugList = JsonSerializer.Deserialize<List<Drug>>(contents);

            string targetFile = System.IO.Path.Combine(FileSystem.Current.AppDataDirectory, "drugdata.json");
            using FileStream outputStream = System.IO.File.OpenWrite(targetFile);
            using StreamWriter streamWriter = new StreamWriter(outputStream);
            await streamWriter.WriteAsync(JsonSerializer.Serialize(drugList));

            return;
        }
    }
}
