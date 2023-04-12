using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jokester.Models
{
    public class JokeAPIModel
    {
        public bool error { get; set; }
        public string category { get; set; }
        public string type { get; set; }
        public string setup { get; set; }
        public string delivery { get; set; }
        public Dictionary<string, bool> flags{ get; set; }
        public int id { get; set; }
        public bool safe { get; set; }
        public string lang { get; set; }
        public string joke { get; set; }
    }
}
