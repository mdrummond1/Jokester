using Jokester.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jokester.Tests.Mocks
{
    internal class MockTextToSpeechService : ITextToSpeechService
    {
        public void SpeakAsync(string text)
        {
            TextToSpeech.SpeakAsync(text);
        }
    }
}
