using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HyraPokedex.Models;
using HyraPokedex.Models.PokeApi;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HyraPokedex.Controllers
{
    public class HyraPokedexController : Controller
    {
        public static HyraPokedexVM pokeVM = new HyraPokedexVM()
        {
            searchPokemon = String.Empty,
            masterListPokemon = new List<Pokemon>()
        };
        public async Task<IActionResult> Index()
        {
            if (pokeVM.masterListPokemon.Count < 1)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync("https://pokeapi.co/api/v2/pokemon/?limit=385/"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        var jsonResponse = JsonConvert.DeserializeObject<PokeApiResponse<Pokemon>>(apiResponse);
                        pokeVM.masterListPokemon = new List<Pokemon>();
                        pokeVM.masterListPokemon.AddRange(jsonResponse.Results);
                        int counter = 1;
                        foreach (Pokemon currPoke in pokeVM.masterListPokemon)
                        {
                            currPoke.ID = counter++;
                        }
                    }
                }
            }
            return View(pokeVM);

        }

        [HttpPost]
        public ActionResult Index(HyraPokedexVM hyraPokedexVM)
        {
            List<Pokemon> filteredPokemon = new List<Pokemon>();
            if (hyraPokedexVM.searchPokemon != String.Empty)
            {
                foreach (Pokemon currPokemon in pokeVM.masterListPokemon)
                {
                    if (currPokemon.Name.Contains(hyraPokedexVM.searchPokemon))
                    {
                        filteredPokemon.Add(currPokemon);
                    }
                }
            }

            if (filteredPokemon.Count > 0)
            {
                return View(new HyraPokedexVM() { masterListPokemon = filteredPokemon });
            }
            else
            {
                return View(pokeVM);
            }
        }

    }
}