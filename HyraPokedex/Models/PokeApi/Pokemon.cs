using Newtonsoft.Json;

namespace HyraPokedex.Models.PokeApi
{
    public class Pokemon
    {
        // URL for pokemon details
        [JsonProperty("url")]
        public string URL { get; set; }

        [JsonProperty("id")]
        public int ID { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("pokemonSprites")]
        public PokemonSprites PokemonSprites { get; set; }
    }
}
