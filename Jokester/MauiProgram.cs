using Jokester.Models;
using Jokester.Services.Impl;
using Jokester.Services.Interfaces;
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

        string filename = $"{Path.Combine(FileSystem.AppDataDirectory, "Person.db3")}";
		List<Type> types = new List<Type>()
		{
			typeof(JokeModel)
		};

        builder.Services.AddSingleton<HttpClient>();
		builder.Services.AddSingleton<IAPIService, APIService>();
		builder.Services.AddSingleton<ITextToSpeechService, DefaultTextToSpeechService>();
		builder.Services.AddSingleton<IDataService>(s => new SqlLiteDataBaseDataService(filename, types));
		builder.Services.AddSingleton((e) => Connectivity.Current);

		builder.Services.AddSingleton<ChuckNorrisJokeViewModel>();
		builder.Services.AddTransient<GeekyJokeViewModel>();
		builder.Services.AddTransient<JokeAPIView>();

		builder.Services.AddSingleton<ChuckNorrisView>();
		builder.Services.AddSingleton<GeekyJokeView>();
		builder.Services.AddSingleton<JokeAPIViewModel>();

		


		return builder.Build();
	}
}
