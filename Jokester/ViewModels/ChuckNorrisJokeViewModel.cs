using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Jokester.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Jokester.ViewModels
{
    public partial class ChuckNorrisJokeViewModel : ObservableObject
    {

        private readonly string BaseAddress = "https://api.chucknorris.io/jokes";
        private HttpClient httpClient;

        [ObservableProperty]
        private ChuckNorrisJoke joke;
        [ObservableProperty]
        private ObservableCollection<string> categories;
        [ObservableProperty]
        private ObservableCollection<ChuckNorrisJoke> foundJokes;
        [ObservableProperty]
        private string selectedCategory;
        [ObservableProperty]
        private string jokeSearchText;
        [ObservableProperty]
        private int numberSearchResults;

        [RelayCommand]
        private void GetChuckNorrisJoke()
        {
            GetRequestResult($"{BaseAddress}/random");
        }


        [RelayCommand]
        private void GetRandomChuckNorrisJokeFromCategory()
        {
            GetRequestResult($"{BaseAddress}/random?category={SelectedCategory}");
        }

        [RelayCommand]
        private void SearchJoke()
        {
            GetRequestResult($"{BaseAddress}/search?query={JokeSearchText}");
        }

        [RelayCommand]
        private void ClearFoundJokes()
        {
            NumberSearchResults = 0;
            JokeSearchText = "";
            FoundJokes.Clear();
        }

        private async void GetRequestResult(string url)
        {
            try
            {
                var res = await httpClient.GetStringAsync(url);
                UpdateJoke(res);    
            }
            catch
            {
                Joke = new ChuckNorrisJoke()
                {
                    value = "Error processing request!"
                };
                JokeSearchText = "";
            }
        }

        private void UpdateJoke(string res)
        {
            Joke = JsonConvert.DeserializeObject<ChuckNorrisJoke>(res);
            if (string.IsNullOrEmpty(Joke.value))
            {
                var searchResults = JsonConvert.DeserializeObject<ChuckNorrisJokeSearchResults>(res);
                NumberSearchResults = searchResults.total;

                FoundJokes = new ObservableCollection<ChuckNorrisJoke>();

                foreach(var joke in searchResults.result)
                {
                    FoundJokes.Add(joke);
                }
                JokeSearchText = "";
            }
            SemanticScreenReader.Announce(Joke.value);
        }

        public ChuckNorrisJokeViewModel()
        {
            httpClient = new HttpClient();
            UpdateCategories();
        }

        private async void UpdateCategories()
        {
            var res = await httpClient.GetStringAsync($"{BaseAddress}/categories");
            var list = JsonConvert.DeserializeObject<List<string>>(res);


            Categories = new ObservableCollection<string>();

            foreach (var cat in list)
            {
                Categories.Add(cat);
            }
        }

    }
}
