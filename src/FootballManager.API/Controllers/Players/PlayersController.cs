using FootballManager.API.Controllers.Players;
using FootballManager.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FootballManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayersController : ControllerBase
    {

        private readonly ILogger<PlayersController> _logger;
        private readonly List<PlayerDto> testPlayerDB;

        public PlayersController(ILogger<PlayersController> logger)
        {
            _logger = logger;
            int id = 1;
            // TEST
            testPlayerDB = Enumerable.Range(1, 5).Select(index => new PlayerDto()
            {
                Id = id++,
                Name = "Aldo",
                Surname = "Baglio",
                RoleAcronym = PlayerRole.Defender.Acronym,
                CurrentTeamId = 0
            }).ToList();
            //TEST
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ListPlayersResponse))]
        [SwaggerOperation(
            Summary = "List all players",
            Description = "Gets the list of players in the whole database",
            OperationId = "players.List")
        ]
        public async Task<ActionResult<ListPlayersResponse>> ListPlayers(CancellationToken cancellationToken)
        {
            var response = new ListPlayersResponse();
            response.Players.AddRange(testPlayerDB);
            return Ok(response);
        }

        [HttpGet("{playerId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetPlayersResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(
            Summary = "Get details about a specific player",
            Description = "Gets details about a specific player",
            OperationId = "players.Get")
        ]
        public async Task<ActionResult<GetPlayersResponse>> GetPlayer([FromRoute] int playerId, CancellationToken cancellationToken)
        {
            var response = new GetPlayersResponse
            {
                Player = testPlayerDB.SingleOrDefault(p => p.Id == playerId)
            };
            if (response.Player != null)
            {
                return Ok(response);
            } 
            else
            {
                return NotFound();
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreatePlayerResponse))]
        [SwaggerOperation(
            Summary = "Add a new player to the database",
            Description = "Adds a new player to the database, assigning a new id to him",
            OperationId = "players.Create")
]
        public async Task<ActionResult<CreatePlayerResponse>> CreatePlayer([FromBody] CreatePlayerRequest createPlayerRequest, CancellationToken cancellationToken)
        {
            var player = new PlayerDto()
            {
                Id = testPlayerDB.Count,
                Name = createPlayerRequest.Name,
                Surname = createPlayerRequest.Surname,
                RoleAcronym = createPlayerRequest.RoleAcronym,
            };
            testPlayerDB.Add(player);
            var response = new CreatePlayerResponse();
            response.Player = player;
            return Ok(response);
        }

        [HttpPost("{playerId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreatePlayerResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(
            Summary = "Update a specific player",
            Description = "Updated a specific player data",
            OperationId = "players.Update")
]
        public async Task<ActionResult<CreatePlayerResponse>> UpdatePlayer([FromRoute] int playerId, [FromBody] UpdatePlayerRequest createPlayerRequest, CancellationToken cancellationToken)
        {
            var player = testPlayerDB.SingleOrDefault(x => x.Id == playerId);
            if (player != null)
            {
                player.Name = createPlayerRequest.Name;
                player.Surname = createPlayerRequest.Surname;
                player.RoleAcronym = createPlayerRequest.RoleAcronym;

                var response = new CreatePlayerResponse
                {
                    Player = player
                };
                return Ok(response);
            } 
            else
            {
                return BadRequest();
            }

            
        }
    }
}
