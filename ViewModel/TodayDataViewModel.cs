using Delta.Model;
using Delta.Services;

namespace Delta.ViewModel
{
    public partial class TodayDataViewModel : BaseViewModel
    {
        TodayDataService todayData;
        public ObservableCollection<TodayData> TodayDatas { get; set; } = new();
        public TodayDataViewModel(TodayDataService todaydataService)
        {
            Title = "Daten Heute";
            this.todayData = todaydataService;
        }

        [RelayCommand]
        async Task GetTodayDatasAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                var todaydatas = await todayData.GetTodayDatas();

                if (TodayDatas.Count != 0)
                    TodayDatas.Clear();

                foreach (var todaydata in todaydatas)
                    TodayDatas.Add(todaydata);
            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error!",
                    $"Unable to get Data: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
