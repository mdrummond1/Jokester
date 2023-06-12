using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jokester.Services.Interfaces
{
    public interface ITextToSpeechService
    {
        void SpeakAsync(string text);

    }

    public class DefaultTextToSpeechService : ITextToSpeechService
    {
        public async void SpeakAsync(string text)
        {
            await TextToSpeech.SpeakAsync(text);
        }
    }
}
