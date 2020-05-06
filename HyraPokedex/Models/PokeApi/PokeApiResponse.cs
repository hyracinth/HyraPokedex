using Newtonsoft.Json;
using System.Collections.Generic;

namespace HyraPokedex.Models.PokeApi
{
    public class PokeApiResponse<T>
    {
        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("next")]
        public string Next { get; set; }

        [JsonProperty("previous")]
        public string Previous { get; set; }

        [JsonProperty("results")]
        public List<T> Results { get; set; }
    }
}
