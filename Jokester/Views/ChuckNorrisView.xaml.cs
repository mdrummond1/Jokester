using Jokester.ViewModels;
using System.Runtime.CompilerServices;

namespace Jokester.Views;

public partial class ChuckNorrisView: ContentPage
{
	public ChuckNorrisView(ChuckNorrisJokeViewModel chuckNorrisJokeViewModel)
	{
		InitializeComponent();
		BindingContext = chuckNorrisJokeViewModel;
        
    }
}
