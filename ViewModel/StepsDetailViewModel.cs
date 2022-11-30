using CommunityToolkit.Mvvm.ComponentModel;
using Delta.Model;
using Delta.Services;
using System.Globalization;

namespace Delta.ViewModel
{
    public partial class StepsDetailViewModel : BaseViewModel
    {
        public ObservableCollection<Steps> Stepss { get; } = new();
        StepsService stepsService;

        public StepsDetailViewModel(StepsService stepsService)
        {
            Title = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(12);
            this.stepsService = stepsService;
        }

        [ObservableProperty]
        bool isRefreshing;

        [RelayCommand]
        async Task GetStepsThisMonthAsync(string month)
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                var stepss = await stepsService.GetStepsByMonthAsync(DateTime.Now.ToString(month));

                if (Stepss.Count != 0)
                    Stepss.Clear();

                foreach (var step in stepss)
                    Stepss.Add(step);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get Data: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }
    }
}
