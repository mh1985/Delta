using Delta.Services;
using Delta.Views;
using Delta.ViewModel;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using SkiaSharp.Views.Maui.Controls.Hosting;

namespace Delta;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
            .UseSkiaSharp(true)
            .UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				fonts.AddFont("FontAwesomeSolid.oft", "FontAwesomeSoild");
			});

		builder.Services.AddSingleton<DrugService>();
		builder.Services.AddSingleton<ShowAllStepsService>();
		builder.Services.AddSingleton<StepsService>();

        builder.Services.AddSingleton<DataPageViewModel>();
        builder.Services.AddSingleton<DrugViewModel>();
		builder.Services.AddSingleton<ShowAllStepsViewModel>();
		builder.Services.AddSingleton<StepsDetailViewModel>();

        builder.Services.AddSingleton<DataPage>();
        builder.Services.AddTransient<DrugAddPage>();
        builder.Services.AddSingleton<DrugPage>();
		builder.Services.AddSingleton<ShowAllStepsPage>();
		builder.Services.AddSingleton<StepsDetailPage>();

        builder.Services.AddTransient<MainPage>();

        return builder.Build();
	}
}
