using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Jokester.Models
{
    public class ChuckNorrisJoke : Entity<string>
    {
        [JsonPropertyName("icon_url")]
        public string Icon_URL { get; set; }
        
        [JsonPropertyName("url")]
        public string URL { get; set; }
        
        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("categories")]
        public string[] Categories { get; set; } 
    }
}
