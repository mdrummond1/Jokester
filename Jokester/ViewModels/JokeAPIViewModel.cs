using CommunityToolkit.Mvvm.ComponentModel;
using Jokester.Models;
using Jokester.Services;
using Microsoft.Maui.Networking;
using Newtonsoft.Json;

namespace Jokester.ViewModels
{
    public partial class JokeAPIViewModel: ObservableObject
    {
        private string url = "https://v2.jokeapi.dev/joke/Any?type=single";
        IAPIService apiService;
        private readonly IConnectivity connectivity;
        [ObservableProperty]
        private JokeAPIModel joke;


        public JokeAPIViewModel(IAPIService apiService, IConnectivity connectivity)
        {
            this.apiService = apiService;
            this.connectivity = connectivity;
        }

        private async Task GetJoke()
        {
            if (connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                joke = new JokeAPIModel()
                {
                    joke = "No network access. Please try again later."
                };
                return;
            }

            var res = await apiService.MakeAPIRequest(url);
            joke = JsonConvert.DeserializeObject<JokeAPIModel>(res);
        }

    }
}
