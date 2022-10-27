using Delta.Services;
using Delta.Views;

namespace Delta;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddSingleton<DrugService>();
		builder.Services.AddSingleton<TodayDataService>();

		builder.Services.AddSingleton<DrugViewModel>();
		builder.Services.AddSingleton<TodayDataViewModel>();

		builder.Services.AddSingleton<MainPage>();
		builder.Services.AddSingleton<DrugPage>();
		builder.Services.AddSingleton<TodayDataPage>();

		return builder.Build();
	}
}
