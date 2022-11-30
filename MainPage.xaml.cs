namespace Delta;

public partial class MainPage : Shell
{
	public MainPage()
	{
		InitializeComponent();

		//TO-DO: Settings-Page erstellen und dahin verlagern
		Preferences.Default.Set("targetsteps", 10000);
	}
}

