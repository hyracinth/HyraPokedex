using HyraPokedex.Models.PokeApi;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HyraPokedex.ViewComponents
{
    public class PokeListViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(List<Pokemon> pokemonList, string pokemonFilter)
        {
            List<Pokemon> filteredList = FilterPokemonList(pokemonList, pokemonFilter);
            return View("PokeList", filteredList);
        }

        public List<Pokemon> FilterPokemonList(List<Pokemon> pokemonList, string pokemonFilter)
        {
            if (!string.IsNullOrEmpty(pokemonFilter))
            {
                return pokemonList.Where(x => x.Name.ToLower().Contains(pokemonFilter)).ToList();
            }
            else
            {
                return pokemonList;
            }
        }
    }
}
