namespace Delta.Views;

public partial class DrugAddPage : ContentPage
{
	public DrugAddPage(DrugViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}