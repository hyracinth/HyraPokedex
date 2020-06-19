using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HyraPokedex.Models.PokeApi
{
    public class FlavorText
    {
        [JsonProperty("flavor_text")]
        public string Text { get; set; }

        [JsonProperty("language")]
        public NamedApiResource Language { get; set; }
        
        [JsonProperty("version")]
        public NamedApiResource Version { get; set; }
    }
}
