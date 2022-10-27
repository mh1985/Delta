namespace Delta.Views;

public partial class TodayDataPage : ContentPage
{
	public TodayDataPage(TodayDataViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}