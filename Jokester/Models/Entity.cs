using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Jokester.Models
{
    public abstract class Entity<T>
    {
        [JsonPropertyName("id"), PrimaryKey]
        public T ID { get; set; }
    }
}
