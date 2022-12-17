using Jokester.ViewModels;
using Jokester.Views;

namespace Jokester;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSansRegular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSansSemibold.ttf", "OpenSansSemibold");
			});


		builder.Services.AddSingleton<ChuckNorrisJokeViewModel>();
		builder.Services.AddSingleton<GeekyJokeViewModel>();

		builder.Services.AddSingleton<ChuckNorrisView>();
		builder.Services.AddSingleton<GeekyJokeView>();


		return builder.Build();
	}
}
