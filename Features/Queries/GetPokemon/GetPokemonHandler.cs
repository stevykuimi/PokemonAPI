using MediatR;
using PokemonAPI.Interfaces;
using PokemonAPI.Models;
using PokemonAPI.Wrappers;

namespace PokemonAPI.Features.Queries.GetPokemon
{
    internal sealed class GetPokemonHandler : IRequestHandler<GetPokemonQuery, PagedResponse<Pokemon>>
    {
        private readonly CSVService _csvService;
        public GetPokemonHandler(CSVService csvService) 
        {
            _csvService = csvService; 
        }
        

        public Task<PagedResponse<Pokemon>> Handle(GetPokemonQuery request, CancellationToken cancellationToken)
        {
            List<Pokemon> response = new ();

            var records = _csvService.GetAllData().Where(P => P.Id == request.PokemonId);

            foreach (Pokemon rec in records)
            {
                response.Add(rec);
            }
            PagedResponse<Pokemon> pagedResponse = new PagedResponse<Pokemon>(response, request.PageNumber, request.PageSize, response.Count);
            return Task.FromResult(pagedResponse);
        }
    }
}