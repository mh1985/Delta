namespace Delta.Views;

public partial class ShowAllStepsPage : ContentPage
{
	public ShowAllStepsPage(ShowAllStepsViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}