//using AndroidX.Navigation;
using Delta.Model;
using Delta.Services;
using Delta.Views;

namespace Delta.ViewModel
{
    public partial class DrugViewModel : BaseViewModel
    {
        DrugService drugService;
        string addDrugName, addDrugDose, addDrugForm, addDrugFrequency, addDrugFrequency2;
        public string AddDrugName { get => addDrugName; set => SetProperty(ref addDrugName, value); }
        public string AddDrugDose { get => addDrugDose; set => SetProperty(ref addDrugDose, value); }
        public string AddDrugForm { get => addDrugForm; set => SetProperty(ref addDrugForm, value); }
        public string AddDrugFrequency { get => addDrugFrequency; set => SetProperty(ref addDrugFrequency, value); }
        public string AddDrugFrequency2 { get => addDrugFrequency2; set => SetProperty(ref addDrugFrequency2, value); }

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
                    $"Unable to get Drugs: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        async Task AddDrugsAsync()
        {
            if (IsBusy)
                return;

            try
            {
                await this.GetDrugsAsync();
                await drugService.AddDrug(addDrugName, addDrugDose, addDrugForm, addDrugFrequency, addDrugFrequency2);
                await this.GetDrugsAsync(); //Ersetzen durch Refresh

                addDrugName = addDrugDose = addDrugForm = addDrugFrequency = addDrugFrequency2 = null;
            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error!",
                    $"Unable to set new drug: {ex.Message}", "OK");
            }

            finally
            {
                IsBusy = false;
                await Shell.Current.GoToAsync("..");
            }
        }

        [RelayCommand]
        Task Navigate() => Shell.Current.GoToAsync(nameof(DrugAddPage));
    }
}