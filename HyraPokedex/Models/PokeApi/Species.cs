using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HyraPokedex.Models.PokeApi
{
    public class Species
    {
        [JsonProperty("id")]
        public string ID { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("flavor_text_entries")]
        public List<FlavorText> FlavorTexts { get; set; }
    }
}
