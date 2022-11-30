namespace Delta.Views;

public partial class StepsDetailPage : ContentPage
{
	public StepsDetailPage(StepsDetailViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}