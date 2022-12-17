using Jokester.ViewModels;

namespace Jokester.Views;

public partial class ChuckNorrisView: ContentPage
{
	public ChuckNorrisView(ChuckNorrisJokeViewModel chuckNorrisJokeViewModel)
	{
		InitializeComponent();
		BindingContext = chuckNorrisJokeViewModel;
	}
}