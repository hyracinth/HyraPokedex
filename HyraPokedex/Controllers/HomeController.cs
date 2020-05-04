using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HyraPokedex.Models.PokeApi;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HyraPokedex.Controllers
{
    public class HomeController : Controller
    {
        List<Pokemon> pokemons = new List<Pokemon>();
        public async Task<IActionResult> Index()
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://pokeapi.co/api/v2/pokemon/?limit=385/"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var jsonResponse = JsonConvert.DeserializeObject<PokeApiResponse<Pokemon>>(apiResponse);
                    pokemons.AddRange(jsonResponse.Results);
                }
            }
            return View(pokemons);
        }
    }
}