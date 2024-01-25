using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MineSweeperDemo.API.Service;
using Newtonsoft.Json;
using System.Text.Json;

namespace MineSweeperDemo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MineSweeperGameController : ControllerBase
    {
        private readonly MineSweeperGameService _gameService;

        public MineSweeperGameController(MineSweeperGameService gameService)
        {
            this._gameService = gameService;
        }

        [HttpGet("generate-grid")]
        public IActionResult GenerateGrid([FromQuery] int grid_size, [FromQuery] int mines)
        {
            var result = _gameService.GenerateGridData(grid_size, grid_size, mines);
            
            if (result != null)
            {
                var resultJson = JsonConvert.SerializeObject(result, Formatting.Indented);

                return Ok(resultJson);
            }

            return BadRequest(500);
        }
    }
}
