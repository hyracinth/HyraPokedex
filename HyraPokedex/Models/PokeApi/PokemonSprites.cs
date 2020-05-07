using Newtonsoft.Json;

namespace HyraPokedex.Models.PokeApi
{
    public class PokemonSprites
    {
        [JsonProperty("front_default")]
        public string FrontDefault { get; set; }
    }
}
