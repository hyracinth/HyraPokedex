using HyraPokedex.Models.PokeApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HyraPokedex.Models
{
    public class HyraPokedexVM
    {
        public string searchPokemon { get; set; }
        public List<Pokemon> masterListPokemon { get; set; }
    }
}
