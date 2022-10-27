namespace Delta.Views;

public partial class DrugPage : ContentPage
{
	public DrugPage(DrugViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}