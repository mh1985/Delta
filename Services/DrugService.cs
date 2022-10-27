using Delta.Model;
using System.Net.Http.Json;

namespace Delta.Services
{
    public class DrugService
    {
        HttpClient httpClient;
        public DrugService()
        {
            httpClient = new HttpClient();
        }

        List<Drug> drugList = new List<Drug>();

        public async Task<List<Drug>> GetDrugs()
        {
            if(drugList?.Count > 0)
                return drugList;

            var url = "https://raw.githubusercontent.com/mh1985/D_Data/main/drugdata.json";

            var response = await httpClient.GetAsync(url);

            if(response.IsSuccessStatusCode)
            {
                drugList = await response.Content.ReadFromJsonAsync<List<Drug>>();
            }

            return drugList;
        }
    }
}
