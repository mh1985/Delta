namespace Delta.Views;

public partial class DataPage : ContentPage
{
	public DataPage(DataPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext= viewModel;
	}
}