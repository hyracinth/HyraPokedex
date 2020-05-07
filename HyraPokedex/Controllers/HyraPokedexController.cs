using HyraPokedex.Models;
using HyraPokedex.Models.PokeApi;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HyraPokedex.Controllers
{
    public class HyraPokedexController : Controller
    {
        private IPokeApiService _pokeApi;

        public static HyraPokedexVM pokeVM = new HyraPokedexVM()
        {
            masterListPokemon = new List<Pokemon>()
        };

        public HyraPokedexController(IPokeApiService pokeApiService)
        {
            _pokeApi = pokeApiService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (pokeVM.masterListPokemon.Count < 1)
            {
                List<Pokemon> listPokemon = await _pokeApi.GetAllPokemon();
                pokeVM.masterListPokemon = new List<Pokemon>();
                pokeVM.masterListPokemon.AddRange(listPokemon);
            }

            // TODO: Extract into helper function?
            int counter = 1;
            foreach (Pokemon currPoke in pokeVM.masterListPokemon)
            {
                currPoke.ID = counter++;
                currPoke.Name = currPoke.Name.ToLower();
            }
            return View(pokeVM);
        }

        [HttpPost]
        public ActionResult Index(HyraPokedexVM hyraPokedexVM, string search, string clear)
        {
            if (!string.IsNullOrEmpty(search))
            {
                pokeVM.searchPokemon = hyraPokedexVM.searchPokemon;
            }
            else
            {
                ResetView();
            }
            return View(pokeVM);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int pokeId)
        {
            // if pokemon data not retrieved yet, retrieve it
            // if pokemon data retrieved, assign
            // else null
            Pokemon existingPokemon = pokeVM.masterListPokemon.First(x => x.ID == pokeId);
            int existingPokeId = -1;
            if (existingPokemon != null && !existingPokemon.DataRetrieved)
            {
                existingPokeId = pokeVM.masterListPokemon.IndexOf(existingPokemon);
                Pokemon updatedPokemon = await _pokeApi.GetPokemonDetail(existingPokemon.URL);
                if (existingPokeId != -1)
                {
                    UpdateMasterList(ref pokeVM, existingPokeId, updatedPokemon);
                    pokeVM.selectedPokemon = pokeVM.masterListPokemon[existingPokeId];
                }
            }
            else if (existingPokemon.DataRetrieved)
            {
                existingPokeId = pokeVM.masterListPokemon.IndexOf(existingPokemon);
                pokeVM.selectedPokemon = pokeVM.masterListPokemon[existingPokeId];
            }
            else
            {
                pokeVM.selectedPokemon = null;
            }

            return RedirectToAction("Index", "HyraPokedex");
        }

        // Reassigning the URL value. Perhaps unnecessary
        private void UpdateMasterList(ref HyraPokedexVM pokeVM, int pokeMasterIndex, Pokemon updatedPokemon)
        {
            string url = pokeVM.masterListPokemon[pokeMasterIndex].URL;
            pokeVM.masterListPokemon[pokeMasterIndex] = updatedPokemon;
            pokeVM.masterListPokemon[pokeMasterIndex].DataRetrieved = true;
            pokeVM.masterListPokemon[pokeMasterIndex].URL = url;
        }

        // Resets form but retains master list
        private void ResetView()
        {
            ModelState.Clear();
            pokeVM.selectedPokemon = null;
            pokeVM.searchPokemon = null;
        }
    }
}