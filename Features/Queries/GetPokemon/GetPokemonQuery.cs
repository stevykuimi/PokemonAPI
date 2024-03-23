using MediatR;
using PokemonAPI.Filters;
using PokemonAPI.Models;
using PokemonAPI.Wrappers;

namespace PokemonAPI.Features.Queries.GetPokemon;
public class GetPokemonQuery: PaginationParameters, IRequest<PagedResponse<Pokemon>>
{
    public int PokemonId { get; set; }

    public GetPokemonQuery(int id, int pageNumber, int pageSize)
    {

        PokemonId = id;
        if (pageNumber != 0)
            PageNumber = pageNumber;
        if (pageSize != 0)
            PageSize = pageSize;

    }

}
