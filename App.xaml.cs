using Delta.Views;

namespace Delta;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();

        //Korrigiert den Fehler, dass bei einem Picker der Titel über dem Picker steht,
        //statt als "Default-Element".
#if WINDOWS10_0_19041_0_OR_GREATER
    Microsoft.Maui.Handlers.PickerHandler.Mapper.AppendToMapping(nameof(IPicker.Title), (handler, view) =>
    {
        if (handler.PlatformView is not null && view is Picker pick && !String.IsNullOrWhiteSpace(pick.Title))
        {
            handler.PlatformView.HeaderTemplate = new Microsoft.UI.Xaml.DataTemplate();			
            handler.PlatformView.PlaceholderText = pick.Title;
            pick.Title = null;
         }
    });
#endif
    }
}
