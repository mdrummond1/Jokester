using Xunit;
using Jokester.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jokester.Tests.Mocks;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Jokester.ViewModels.Tests
{
    public class ChuckNorrisJokeViewModelTests
    {
        ChuckNorrisJokeViewModel viewModel;
        MockAPIService mockAPI;
        ConnectivityMock connectivityMock;
        List<string> jokeCategories = new List<string>()
        {
            "animal",
            "career",
            "celebrity",
            "dev",
            "explicit",
            "fashion",
            "food",
            "history",
            "money",
            "movie",
            "music",
            "political",
            "religion",
            "science",
            "sport",
            "travel"
        };

        public ChuckNorrisJokeViewModelTests() 
        {
            mockAPI = new MockAPIService();
            connectivityMock = new ConnectivityMock();
            mockAPI.RequestReturnValue = JsonConvert.SerializeObject(jokeCategories);
            viewModel = new ChuckNorrisJokeViewModel(mockAPI, new MockTextToSpeechService(), connectivityMock);
        }

        [Fact]
        public void GetJokeCommandExecutes()
        {
            connectivityMock.NetworkAccess = NetworkAccess.Internet;
            var joke = new Models.ChuckNorrisJoke()
            {
                Icon_URL = "",
                ID = "",
                URL = "",
                Value = "This is a joke...haha"
            };

            mockAPI.RequestReturnValue = JsonConvert.SerializeObject(joke);
            viewModel.GetChuckNorrisJokeCommand.Execute(null);
            Assert.Equal(joke.Value, viewModel.Joke.Value);
        }

        [Fact]
        public void GetJokeCommandFails_WithoutNetworkAccess()
        {
            connectivityMock.NetworkAccess = NetworkAccess.None;
            viewModel.GetChuckNorrisJokeCommand.Execute(null);

            Assert.Equal(Messages.NetworkErrorMessage, viewModel.Joke.Value);
        }
    }
}