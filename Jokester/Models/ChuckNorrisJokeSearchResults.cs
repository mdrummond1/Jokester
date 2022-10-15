using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jokester.Models
{
    public class ChuckNorrisJokeSearchResults
    {
        public int total { get; set; }
        public IEnumerable<ChuckNorrisJoke> result { get; set; }
    }
}
