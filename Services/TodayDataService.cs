using Delta.Model;
using System.Net.Http.Json;

namespace Delta.Services
{
    public class TodayDataService
    {
        HttpClient httpClient;
        public TodayDataService()
        {
            httpClient = new HttpClient();
        }

        List<TodayData> todaydataList = new List<TodayData>();

        public async Task<List<TodayData>> GetTodayDatas()
        {
            if (todaydataList?.Count > 0)
                return todaydataList;

            var url = "https://raw.githubusercontent.com/mh1985/D_Data/main/todaydata.json";

            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                todaydataList = await response.Content.ReadFromJsonAsync<List<TodayData>>();
            }

            return todaydataList;
        }
    }
}
