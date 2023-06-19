using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jokester.Models
{
    public class JokeModel: Entity<int>
    {
        public string joke { get; set; }
    }
}
