using Jokester.Views;

namespace Jokester;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(ChuckNorrisView), typeof(ChuckNorrisView));
        Routing.RegisterRoute(nameof(GeekyJokeView), typeof(GeekyJokeView));
    }
}
