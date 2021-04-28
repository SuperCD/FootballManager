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
    [Route("api/Teams/{teamId}/[controller]")]
    public class FormationController : ControllerBase
    {

        private readonly ILogger<FormationController> _logger;

        public FormationController(ILogger<FormationController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ListRoosterResponse))]
        [SwaggerOperation(
            Summary = "Get Formation",
            Description = "Gets the teams formation",
            OperationId = "Formation.List")
        ]
        public async Task<ActionResult<ListRoosterResponse>> ListFormation([FromRoute] int teamId, CancellationToken cancellationToken)
        {
            var response = new ListRoosterResponse();
            {
                response.TeamId = teamId;
            }
            return Ok(response);
        }

        [HttpPut("{playerId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AddPlayerToRoosterResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(
            Summary = "Add Player to formation",
            Description = "Adds a team player to the formation in the first compatible position (if available)",
            OperationId = "Formation.Add")
        ]
        public async Task<ActionResult<AddPlayerToRoosterResponse>> AddPlayerToFormation([FromRoute] int teamId, [FromRoute] int playerId, CancellationToken cancellationToken)
        {
            var response = new AddPlayerToRoosterResponse();
            {
                response.TeamId = teamId;
                response.PlayerId = playerId;
            }
            return Ok(response);
        }

        [HttpPut("{positionId}/{playerId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AddPlayerToRoosterResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(
            Summary = "Add Player to a position",
            Description = "Adds a team player to the formation in the specific position",
            OperationId = "Formation.Add")
        ]
        public async Task<ActionResult<AddPlayerToRoosterResponse>> AddPlayerToPosition([FromRoute] int teamId, [FromRoute] int positionId, [FromRoute] int playerId, CancellationToken cancellationToken)
        {
            var response = new AddPlayerToRoosterResponse();
            {
                response.TeamId = teamId;
                response.PlayerId = playerId;
            }
            return Ok(response);
        }
    }
}
