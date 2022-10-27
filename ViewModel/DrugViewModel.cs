using Delta.Model;
using Delta.Services;

namespace Delta.ViewModel
{
    public partial class DrugViewModel : BaseViewModel
    {
        DrugService drugService;
        public ObservableCollection<Drug> Drugs { get; set; } = new();
        public DrugViewModel(DrugService drugService)
        {
            Title = "Medikamente";
            this.drugService = drugService;
        }

        [RelayCommand]
        async Task GetDrugsAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                var drugs = await drugService.GetDrugs();

                if (Drugs.Count != 0)
                    Drugs.Clear();

                foreach(var drug in drugs)
                    Drugs.Add(drug);
            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error!",
                    $"Unable to get Drugs: {ex.Message}","OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
