using Microsoft.AspNetCore.Mvc;
using PlayerCrudApi.Dto;
using PlayerCrudApi.Players.Model;

namespace PlayerCrudApi.Players.Controller.interfaces
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public abstract class PlayerApiController:ControllerBase
    {
        [HttpGet("all")]
        [ProducesResponseType(statusCode: 200, type: typeof(IEnumerable<Player>))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<ListPlayerDto>> GetAll();

        [HttpPost("create")]
        [ProducesResponseType(statusCode: 201, type: typeof(Player))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<PlayerDto>> CreatePlayer([FromBody] CreatePlayerRequest request);

        [HttpPut("update/{id}")]
        [ProducesResponseType(statusCode: 202, type: typeof(Player))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<PlayerDto>> UpdatePlayer([FromRoute] int id, [FromBody] UpdatePlayerRequest request);

        [HttpDelete("delete/{id}")]
        [ProducesResponseType(statusCode: 202, type: typeof(Player))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<PlayerDto>> DeletePlayer([FromRoute] int id);

        [HttpGet("name/{name}")]
        [ProducesResponseType(statusCode: 202, type: typeof(Player))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<PlayerDto>> GetByNameRoute([FromRoute] string name);
        
        [HttpGet("id/{id}")]
        [ProducesResponseType(statusCode: 202, type: typeof(Player))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<PlayerDto>> GetByIdRoute([FromRoute] int id);

    }
}
