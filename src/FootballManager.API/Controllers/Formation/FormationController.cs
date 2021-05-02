using AutoMapper;
using FootballManager.Domain.Entities;
using FootballManager.Domain.Interfaces;
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
        private readonly ITeamRepository _teamsRepository;
        private readonly IAsyncRepository<Player> _playersRepository;
        private readonly IMapper _mapper;

        public FormationController(ILogger<FormationController> logger,
            IMapper mapper,
            ITeamRepository teamsRepository,
            IAsyncRepository<Player> playersRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _teamsRepository = teamsRepository;
            _playersRepository = playersRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ListFormationResponse))]
        [SwaggerOperation(
            Summary = "Get Formation",
            Description = "Gets the teams formation",
            OperationId = "Formation.List")
        ]
        public async Task<ActionResult<ListFormationResponse>> ListFormation([FromRoute] int teamId, CancellationToken cancellationToken)
        {
            var team = await _teamsRepository.GetByIdWithFormationAsync(teamId);
            var response = new ListFormationResponse();
            {
                response.TeamId = teamId;
                response.Positions = _mapper.Map<List<FormationPositionDto>>(team.Formation.Postitions);
            }
            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AddPlayerToFormationResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(
            Summary = "Add Player to formation",
            Description = "Adds a team player to the formation in the first compatible position (if available)",
            OperationId = "Formation.AddPlayer")
        ]
        public async Task<ActionResult<AddPlayerToFormationResponse>> AddPlayerToFormation([FromRoute] int teamId, [FromBody] PlayerOperationRequest request, CancellationToken cancellationToken)
        {
            var team = await _teamsRepository.GetByIdWithFormationAsync(teamId);
            var player = await _playersRepository.GetByIdAsync(request.PlayerId);
            team.Formation.AddPlayer(player);

            await _teamsRepository.UpdateAsync(team);

            var response = new AddPlayerToFormationResponse();
            {
                response.TeamId = teamId;
                response.PlayerId = request.PlayerId;
                response.PositionNo = team.Formation.Postitions.Single(x => x.Player?.Id == request.PlayerId).PositionNo;
            }
            return Ok(response);
        }

        [HttpPut("{positionNo}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AddPlayerToFormationResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(
            Summary = "Add Player to a position",
            Description = "Adds a team player to the formation in the specific position",
            OperationId = "FormationPosition.AddPlayer")
        ]
        public async Task<ActionResult<AddPlayerToFormationResponse>> AddPlayerToPosition([FromRoute] int teamId, [FromRoute] int positionNo, [FromBody] PlayerOperationRequest request, CancellationToken cancellationToken)
        {
            var team = await _teamsRepository.GetByIdWithFormationAsync(teamId);
            var player = await _playersRepository.GetByIdAsync(request.PlayerId);
            team.Formation.AddPlayer(player, positionNo);

            await _teamsRepository.UpdateAsync(team);

            var response = new AddPlayerToFormationResponse();
            {
                response.TeamId = teamId;
                response.PlayerId = request.PlayerId;
                response.PositionNo = positionNo;
            }
            return Ok(response);
        }

        [HttpDelete("{positionNo}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(
            Summary = "Empty Position",
            Description = "Removes any player from the position and makes it empty",
            OperationId = "FormationPosition.Empty")
        ]
        public async Task<StatusCodeResult> EmptyPosition([FromRoute] int teamId, [FromRoute] int positionNo, CancellationToken cancellationToken)
        {
            var team = await _teamsRepository.GetByIdWithFormationAsync(teamId);
            team.Formation.EmptyPosition(positionNo);
            await _teamsRepository.UpdateAsync(team);
            return Ok();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(
            Summary = "Remove Player",
            Description = "Removes a specific player from the formation if he is in it",
            OperationId = "Formation.RemovePlayer")
        ]
        public async Task<StatusCodeResult> RemovePlayer([FromRoute] int teamId, [FromBody] PlayerOperationRequest request, CancellationToken cancellationToken)
        {
            var team = await _teamsRepository.GetByIdWithFormationAsync(teamId);
            var player = await _playersRepository.GetByIdAsync(request.PlayerId);
            team.Formation.RemovePlayer(player);
            await _teamsRepository.UpdateAsync(team);
            return Ok();
        }

    }
}
