using Jokester.ViewModels;

namespace Jokester.Views;

public partial class JokeAPIView : ContentPage
{
	public JokeAPIView(JokeAPIViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}