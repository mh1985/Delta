using Delta.Services;
using Delta.Model;
using Delta.Views;

namespace Delta.ViewModel
{
    public partial class ShowAllStepsViewModel : BaseViewModel
    {
        StepsService stepsService;
        public ObservableCollection<Drug> Drugs { get; set; } = new();
        public ObservableCollection<Steps> Stepss { get; set; } = new();

        public ShowAllStepsViewModel(StepsService stepsService)
        {
            Title = "Alle Steps in DB";
            this.stepsService = stepsService;
        }

        [RelayCommand]
        async Task GetStepssAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                var stepss = await stepsService.GetStepsAsync();

                if (Stepss.Count != 0)
                    Stepss.Clear();

                foreach (var steps in stepss)
                    Stepss.Add(steps);
            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error!",
                    $"Unable to get Drugs: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        async Task DeleteTableAsync()
        {
            await stepsService.DropTable();
        }
    }
}
