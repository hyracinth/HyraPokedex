namespace HyraPokedex
{
    public static class PokeApiEnum
    {
        public const string BASE_URL = "https://pokeapi.co/api/v2";
        public const string POKEMON = "/pokemon";
        public const string UNKNOWN_QUERY = "/?limit=";
        public const int MAX_POKEMON_ID = 385;

        public static readonly string GET_ALL_POKEMON = BASE_URL + POKEMON + UNKNOWN_QUERY + MAX_POKEMON_ID;
    }
}
