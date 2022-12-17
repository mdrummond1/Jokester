using Jokester.ViewModels;

namespace Jokester.Views;

public partial class GeekyJokeView : ContentPage
{
	public GeekyJokeView(GeekyJokeViewModel geekyJokeViewModel)
	{
		InitializeComponent();
		BindingContext= geekyJokeViewModel;
	}
}