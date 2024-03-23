using Microsoft.AspNetCore.Mvc;
using PokemonAPI.Models;
using MediatR;
using PokemonAPI.Wrappers;
using PokemonAPI.Features.Queries.GetPokemon;
using PokemonAPI.Interfaces;

namespace PokemonAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonsController : ControllerBase
    {
        private readonly CSVService _csvService;
        private readonly IMediator _mediator;

        public PokemonsController(CSVService csvService, IMediator mediator)
        {
            _csvService = csvService;
            _mediator = mediator;
        }


        [HttpGet]
        public IActionResult GetAllData(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var data = _csvService.GetAllData();
                PagedResponse<Pokemon> paginatedData = new PagedResponse<Pokemon>(data, pageNumber, pageSize, data.Count);
                return Ok(paginatedData);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{pokemonId}")]
        public async Task<ActionResult> GetDataById(
            [FromRoute] int pokemonId,
            [FromQuery] int? pageNumber,
            [FromQuery] int? pageSize)
        {
            return Ok( await _mediator.Send(new GetPokemonQuery(pokemonId, pageNumber ?? 0,pageSize ?? 0)));
        }



        [HttpPost]
        public IActionResult AddData(Pokemon data)
        {
            try
            {
                _csvService.AddData(data);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpPut("{id}")]
        public IActionResult UpdateData(int id, Pokemon newData)
        {
            try
            {
                _csvService.UpdateData(id, newData);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteData(int id)
        {
            try
            {
                _csvService.DeleteData(id);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
