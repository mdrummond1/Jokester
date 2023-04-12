using Jokester.Services;
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

		builder.Services.AddSingleton<HttpClient>();
		builder.Services.AddSingleton<IAPIService, APIService>();
		builder.Services.AddSingleton<ITextToSpeechService, DefaultTextToSpeechService>();

		builder.Services.AddSingleton<ChuckNorrisJokeViewModel>();
		builder.Services.AddTransient<GeekyJokeViewModel>();
		builder.Services.AddTransient<JokeAPIView>();

		builder.Services.AddSingleton<ChuckNorrisView>();
		builder.Services.AddSingleton<GeekyJokeView>();
		builder.Services.AddSingleton<JokeAPIViewModel>();

		


		return builder.Build();
	}
}
