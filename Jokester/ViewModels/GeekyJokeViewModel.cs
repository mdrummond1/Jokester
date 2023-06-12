using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Jokester.Models;
using Jokester.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Jokester.ViewModels
{
    public partial class GeekyJokeViewModel: ObservableObject
    {
        private string url = "https://geek-jokes.sameerkumar.website/api?format=json";

        private IAPIService apiService;
        private IConnectivity connectivity;

        [ObservableProperty]
        private JokeModel joke;

        [ObservableProperty]
        private ObservableCollection<string> categories;


        [RelayCommand]
        private async void GetJoke()
        {
            if (connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                Joke = new JokeModel()
                {
                    joke = "No Network Access. Please try again later."
                };
                return;
            }
            var res = await apiService.MakeAPIRequest(url);
            Joke = JsonConvert.DeserializeObject<JokeModel>(res); 
        }

        public GeekyJokeViewModel(IAPIService apiService, IConnectivity connectivity)
        {
            this.apiService = apiService;
            this.connectivity = connectivity;
        }

    }
}
