using Delta.Views;
using Delta.Model;
using Delta.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;

namespace Delta.ViewModel
{
    public partial class DataPageViewModel : BaseViewModel
    {
        StepsService stepsService;
        public ObservableCollection<Steps> TodaySteps { get; set; } = new();
        public string Steps { get; set; }
        public string Walkeddistance { get; set; }

        public DataPageViewModel(StepsService stepsService)
        {
            Title = DateTime.Now.ToString("dddd, dd. MMMM yyyy");
            this.stepsService = stepsService;
        }

        [ObservableProperty]
        bool isRefreshing;

        [RelayCommand]
        public async Task Refresh()
        {
            TodaySteps.Clear();

            var todaySteps = await stepsService.GetStepsByDateAsync(DateTime.Now.ToString("dd.M.yyyy"));
            TodaySteps.Add(todaySteps);

            this.Steps = $"{todaySteps.Stepsleft}";
            this.Walkeddistance = $"{todaySteps.Walkdistance}";

            IsRefreshing = false;
        }

        //Soll ein Diagramm erstellen. Funktioniert nicht.
        public ISeries[] Series { get; set; } = new ISeries[]
            {
            new LineSeries<double>
            {
                Values = new double[] { 2, 1, 3, 5, 3, 4, 6 }, Fill = null
            }
        };

        //Navigationstest
        [RelayCommand]
        Task Navigate() => Shell.Current.GoToAsync(nameof(StepsDetailPage));

        [RelayCommand]
        public async Task CreateSteps()
        {
            //Routine um Tabelle mit Dummy-Daten zu füllen.
            for (int i = 2; i < 31; i += 2)
            {
                Random rnd = new();
                int totalsteps = rnd.Next(0, 16000);
                string Date;

                if (i > 9)
                {
                    Date = i + ".11.2022";
                }
                else
                {
                    Date = "0" + i + ".11.2022";
                }

                try
                {
                    await stepsService.SaveItemAsync(totalsteps, Date);
                }

                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    await Shell.Current.DisplayAlert("Error!",
                        $"Unable: {ex.Message}", "OK");
                }
            }
        }
    }
}
