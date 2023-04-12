using CommunityToolkit.Mvvm.ComponentModel;
using Jokester.Models;
using Jokester.Services;
using Newtonsoft.Json;

namespace Jokester.ViewModels
{
    public partial class JokeAPIViewModel: ObservableObject
    {
        private string url = "https://v2.jokeapi.dev/joke/Any?type=single";
        IAPIService apiService;

        [ObservableProperty]
        private JokeAPIModel jokeAPIModel;


        public JokeAPIViewModel(IAPIService apiService)
        {
            this.apiService = apiService;
            GetJoke();
        }

        private async Task GetJoke()
        {
            var res = await apiService.MakeAPIRequest(url);
            jokeAPIModel = JsonConvert.DeserializeObject<JokeAPIModel>(res);
        }

    }
}
