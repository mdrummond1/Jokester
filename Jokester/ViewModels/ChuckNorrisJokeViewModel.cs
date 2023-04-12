using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Jokester.Models;
using Jokester.Services;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace Jokester.ViewModels
{
    public partial class ChuckNorrisJokeViewModel : ObservableObject
    {

        private readonly string BaseAddress = "https://api.chucknorris.io/jokes";
        private readonly IAPIService apiService;
        private readonly ITextToSpeechService textToSpeechService;

        [ObservableProperty, NotifyCanExecuteChangedFor(nameof(TellJokeCommand))]
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
        [ObservableProperty]
        private ChuckNorrisJoke selectedFoundJoke;


        [RelayCommand]
        private void GetChuckNorrisJoke()
        {
            GetRequestResult($"{BaseAddress}/random");
        }

        [RelayCommand]
        private void GetRandomChuckNorrisJokeFromCategory()
        {
            if (selectedCategory is null)
            {
                Joke = new ChuckNorrisJoke()
                {
                    Value = "Must select a category "
                };
                return;
            }

            GetRequestResult($"{BaseAddress}/random?category={SelectedCategory}");
        }

        [RelayCommand]
        private void SearchJoke()
        {
            if (string.IsNullOrEmpty(JokeSearchText))
            {
                Joke = new ChuckNorrisJoke()
                {
                    Value = "Search must not be blank!"
                };
                return;
            }

            GetRequestResult($"{BaseAddress}/search?query={JokeSearchText}");
        }

        [RelayCommand]
        private void ClearFoundJokes()
        {
            if (FoundJokes is null)
            {
                return;
            }

            NumberSearchResults = 0;
            JokeSearchText = "";
            FoundJokes.Clear();
            SelectedFoundJoke = null;
        }

        [RelayCommand(CanExecute = nameof(CanTellJoke))]
        private void TellJoke()
        {
            textToSpeechService.SpeakAsync(Joke.Value);
        }

        private bool CanTellJoke()
        {
            return Joke is not null;
        }

        private async void GetRequestResult(string url)
        {
            try
            {
                var res = await apiService.MakeAPIRequest(url);
                UpdateJoke(res);
            }
            catch
            {
                Joke = new ChuckNorrisJoke()
                {
                    Value = "Error processing request!"
                };
                JokeSearchText = "";
            }
        }

        private void UpdateJoke(string res)
        {
            Joke = JsonConvert.DeserializeObject<ChuckNorrisJoke>(res);
            if (string.IsNullOrEmpty(Joke.Value))
            {
                var searchResults = JsonConvert.DeserializeObject<ChuckNorrisJokeSearchResults>(res);
                NumberSearchResults = searchResults.total;

                FoundJokes = new ObservableCollection<ChuckNorrisJoke>();

                if (searchResults.total > 0)
                {
                    foreach (var joke in searchResults.result)
                    {
                        FoundJokes.Add(joke);
                    }
                }
                JokeSearchText = "";
            }
        }

        public ChuckNorrisJokeViewModel(IAPIService apiService, ITextToSpeechService textToSpeech)
        {
            this.apiService = apiService;
            this.textToSpeechService = textToSpeech;
            UpdateCategories();
            
        }

        private async void UpdateCategories()
        {
            var res = await apiService.MakeAPIRequest($"{BaseAddress}/categories");
            var list = JsonConvert.DeserializeObject<List<string>>(res);


            Categories = new ObservableCollection<string>();

            foreach (var cat in list)
            {
                Categories.Add(cat);
            }
        }
    }
}
