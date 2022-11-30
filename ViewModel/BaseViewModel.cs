using CommunityToolkit.Mvvm.ComponentModel;
using Delta.Views;

namespace Delta.ViewModel
{
    public partial class BaseViewModel : ObservableObject
    {
        public int targetsteps { get; set; }
        public BaseViewModel()
        {
            targetsteps = Preferences.Default.Get("targetsteps", 2000);
        }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        bool isBusy;

        [ObservableProperty]
        string title;

        public bool IsNotBusy => !IsBusy;
    }
}