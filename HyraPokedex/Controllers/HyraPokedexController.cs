using HyraPokedex.Models;
using HyraPokedex.Models.PokeApi;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace HyraPokedex.Controllers
{
    public class HyraPokedexController : Controller
    {
        public static HyraPokedexVM pokeVM = new HyraPokedexVM()
        {
            masterListPokemon = new List<Pokemon>()
        };
        public async Task<IActionResult> Index()
        {
            if (pokeVM.masterListPokemon.Count < 1)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(PokeApiEnum.GET_ALL_POKEMON))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        var jsonResponse = JsonConvert.DeserializeObject<PokeApiResponse<Pokemon>>(apiResponse);
                        pokeVM.masterListPokemon = new List<Pokemon>();
                        pokeVM.masterListPokemon.AddRange(jsonResponse.Results);
                        int counter = 1;
                        foreach (Pokemon currPoke in pokeVM.masterListPokemon)
                        {
                            currPoke.ID = counter++;
                            currPoke.Name = currPoke.Name.ToLower();
                        }
                    }
                }
            }
            return View(pokeVM);
        }

        [HttpPost]
        public ActionResult Index(HyraPokedexVM hyraPokedexVM, string search, string clear)
        {
            // if search is triggered
            // else return all pokemon and reset
            if (!string.IsNullOrEmpty(search))
            {
                List<Pokemon> filteredPokemon = new List<Pokemon>();
                // aggregate list of pokemon based on user input
                if (!string.IsNullOrEmpty(hyraPokedexVM.searchPokemon))
                {
                    string searchPokemon = hyraPokedexVM.searchPokemon.ToLower();
                    foreach (Pokemon currPokemon in pokeVM.masterListPokemon)
                    {
                        if (currPokemon.Name.Contains(searchPokemon))
                        {
                            filteredPokemon.Add(currPokemon);
                        }
                    }
                }

                // if there are pokemon that match user input, then return filtered list
                // else return full list with error message
                if (filteredPokemon.Count > 0)
                {
                    return View(new HyraPokedexVM()
                    {
                        masterListPokemon = filteredPokemon
                    });
                }
                else
                {
                    return View(new HyraPokedexVM()
                    {
                        masterListPokemon = pokeVM.masterListPokemon,
                        statusMessage = "No results found."
                    });
                }
            }
            else
            {
                ModelState.Clear();
                return View(new HyraPokedexVM()
                {
                    searchPokemon = String.Empty,
                    masterListPokemon = pokeVM.masterListPokemon,
                });
            }
        }
    }
}