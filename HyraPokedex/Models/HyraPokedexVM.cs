using HyraPokedex.Models.PokeApi;
using System.Collections.Generic;

namespace HyraPokedex.Models
{
    public class HyraPokedexVM
    {
        public string searchPokemon { get; set; }
        public string statusMessage { get; set; }
        public List<Pokemon> masterListPokemon { get; set; }
        public Pokemon selectedPokemon { get; set; }
    }
}
