using HyraPokedex.Models.PokeApi;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace HyraPokedex
{
    public interface IPokeApiService
    {
        Task<List<Pokemon>> GetAllPokemon();
        Task<Pokemon> GetPokemonDetail(string url);
    }
    public class PokeApiService : IPokeApiService
    {
        public async Task<List<Pokemon>> GetAllPokemon()
        {
            List<Pokemon> listPokemon = new List<Pokemon>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(PokeApiEnum.GET_ALL_POKEMON))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    PokeApiResponse<Pokemon> jsonResponse = JsonConvert.DeserializeObject<PokeApiResponse<Pokemon>>(apiResponse);
                    listPokemon.AddRange(jsonResponse.Results);
                }
            }
            return listPokemon;
        }

        public async Task<Pokemon> GetPokemonDetail(string url)
        {
            Pokemon pokemonResp = null;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(url))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    pokemonResp = JsonConvert.DeserializeObject<Pokemon>(apiResponse);
                }
            }
            pokemonResp.Species = await GetPokemonSpecies(pokemonResp.ApiResSpecies.URL);
            return pokemonResp;
        }

        public async Task<Species> GetPokemonSpecies(string url)
        {
            Species speciesResp = null;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(url))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    speciesResp = JsonConvert.DeserializeObject<Species>(apiResponse);
                }
            }
            return speciesResp;
        }
    }
}
