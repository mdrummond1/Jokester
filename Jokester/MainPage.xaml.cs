using Jokester.ViewModels;

namespace Jokester;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
		BindingContext = new ChuckNorrisJokeViewModel();
	}
}

