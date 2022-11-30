using Delta.Services;

namespace Delta;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(Views.DrugAddPage), typeof(Views.DrugAddPage));
        Routing.RegisterRoute(nameof(Views.ShowAllStepsPage), typeof(Views.ShowAllStepsPage));
        Routing.RegisterRoute(nameof(Views.StepsDetailPage), typeof(Views.StepsDetailPage));
    }
}
