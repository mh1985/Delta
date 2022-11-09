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

            using var stream = await FileSystem.OpenAppPackageFileAsync("drugdata.json");
            using var reader = new StreamReader(stream);
            var contents = await reader.ReadToEndAsync();

            drugList = JsonSerializer.Deserialize<List<Drug>>(contents);

            return drugList;
        }

        public async void AddDrug(string name, string dose, string form, string frequency, string frequency2)
        {
            bool testbool;
            var drug = new Drug
            {
                Name = name,
                Dose = dose,
                Form = form,
                Frequency = frequency,
                Frequency2 = frequency2
            };

            drugList.Add(drug);

            //using StreamWriter sw = new StreamWriter(Path.Combine(FileSystem.AppDataDirectory, "drugdata.json"));
            string targetFile = System.IO.Path.Combine(FileSystem.Current.AppDataDirectory, "drugdata.json");
            testbool = await FileSystem.AppPackageFileExistsAsync("drugdata.json");

            using FileStream outputStream = System.IO.File.OpenWrite(targetFile);
            using StreamWriter sw = new StreamWriter(outputStream);

            await sw.WriteAsync(JsonSerializer.Serialize(drug));
            //await sw.WriteLineAsync("TestLine");
        }
    }
}
