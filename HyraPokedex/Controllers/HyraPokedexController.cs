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
            if(!string.IsNullOrEmpty(search))
            {
                pokeVM.searchPokemon = hyraPokedexVM.searchPokemon;
            }
            else
            {
                ModelState.Clear();
                pokeVM.searchPokemon = null;
            }
            return View(pokeVM);
        }

        public async Task<IActionResult> Details(int pokeId)
        {
            pokeVM.selectedPokemonId = pokeId;

            Pokemon tempPokemon = null;
            foreach(Pokemon currPokemon in pokeVM.masterListPokemon)
            {
                if(currPokemon.ID == pokeId)
                {
                    tempPokemon = currPokemon;
                    break;
                }
            }

            if (tempPokemon != null && !tempPokemon.DataRetrieved) {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(tempPokemon.URL))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        var jsonResponse = JsonConvert.DeserializeObject<Pokemon>(apiResponse);
                        for (int ii = 0; ii < pokeVM.masterListPokemon.Count; ii++)
                        {
                            if (pokeVM.masterListPokemon[ii].ID == pokeId)
                            {
                                pokeVM.masterListPokemon[ii] = jsonResponse;
                                pokeVM.masterListPokemon[ii].DataRetrieved = true;
                                pokeVM.selectedPokemon = pokeVM.masterListPokemon[ii];
                                break;
                            }
                        }
                    }
                }
            } 
            else if(tempPokemon.DataRetrieved)
            {
                pokeVM.selectedPokemon = pokeVM.masterListPokemon[pokeId - 1];
            }

            return RedirectToAction("Index", "HyraPokedex");
        }
    }
}