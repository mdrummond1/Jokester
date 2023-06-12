using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Jokester.Models;
using Jokester.Services.Interfaces;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace Jokester.ViewModels
{
    public partial class ChuckNorrisJokeViewModel : ObservableObject
    {
        #region Fields

        private readonly string BaseAddress = "https://api.chucknorris.io/jokes";
        private readonly IAPIService apiService;
        private readonly ITextToSpeechService textToSpeechService;
        private readonly IConnectivity connectivity;
        private readonly IDataService dataService;
        private ProfanityFilter.ProfanityFilter filter;

        #endregion

        #region Bindings

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

        #endregion
        
        public ChuckNorrisJokeViewModel(
            IAPIService apiService,
            ITextToSpeechService textToSpeech,
            IConnectivity connectivity,
            IDataService dataService)
        {
            this.apiService = apiService;
            this.textToSpeechService = textToSpeech;
            this.connectivity = connectivity;
            this.dataService = dataService;

            UpdateCategories();

            filter = new ProfanityFilter.ProfanityFilter();
        }

        #region Commands

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

        #endregion

        #region Private Methods

        private bool CanTellJoke()
        {
            return Joke is not null;
        }

        private async void GetRequestResult(string url)
        {
            if (connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                joke = new ChuckNorrisJoke()
                {
                    Value = Messages.NetworkErrorMessage
                };

                return;
            }

            try
            {
                string res = "";
                int b;
                do
                {
                    res = await apiService.MakeAPIRequest(url);
                    b = filter.DetectAllProfanities(res).Count;
                } while (b > 0);
                                                
                UpdateJoke(res);
            }
            catch
            {
                Joke = new ChuckNorrisJoke()
                {
                    Value = Messages.GeneralErrorMessage
                };
                JokeSearchText = "";
            }
        }

        private void UpdateJoke(string res)
        {
            Joke = JsonConvert.DeserializeObject<ChuckNorrisJoke>(res);
            Joke.Value = filter.CensorString(Joke.Value);

            if (string.IsNullOrEmpty(Joke.Value))
            {
                var searchResults = JsonConvert.DeserializeObject<ChuckNorrisJokeSearchResults>(res);
                NumberSearchResults = searchResults.total;

                FoundJokes = new ObservableCollection<ChuckNorrisJoke>();

                if (searchResults.total > 0)
                {
                    foreach (var joke in searchResults.result)
                    {
                        joke.Value = filter.CensorString(joke.Value);
                        FoundJokes.Add(joke);
                    }
                }
                JokeSearchText = "";
            }
        }

        private async void UpdateCategories()
        {
            if (connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                joke = new ChuckNorrisJoke()
                {
                    Value = Messages.NetworkErrorMessage
                };
                            
                return;
            }

            var res = await apiService.MakeAPIRequest($"{BaseAddress}/categories");
            var list = JsonConvert.DeserializeObject<List<string>>(res);
            list.Remove("explicit");

            Categories = new ObservableCollection<string>();

            foreach (var cat in list)
            {
                Categories.Add(cat);
            }
        }

        #endregion
    }
}
