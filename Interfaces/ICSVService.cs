using PokemonAPI.Models;

namespace PokemonAPI.Interfaces
{
    public interface ICSVService
    {
        List<Pokemon> GetAllData();
        Pokemon GetDataById(int id);


    }
}
