using AutoMapper;
using FootballManager.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FootballManager.Domain.Interfaces;
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
    public class TeamsController : ControllerBase
    {

        private readonly ILogger<TeamsController> _logger;
        private readonly IMapper _mapper;
        private readonly ITeamRepository _teamsRepository;
        public TeamsController(ITeamRepository teamsRepository,
            IMapper mapper,
            ILogger<TeamsController> logger)
        {
            _logger = logger;
            _mapper = mapper;
            _teamsRepository = teamsRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ListTeamsResponse))]
        [SwaggerOperation(
            Summary = "List all Teams",
            Description = "Gets the list of Teams in the whole database",
            OperationId = "Teams.List")
        ]
        public async Task<ActionResult<ListTeamsResponse>> ListTeams(CancellationToken cancellationToken)
        {
            var response = new ListTeamsResponse();
            var dbList = await _teamsRepository.ListAsync(cancellationToken);
            response.Teams.AddRange(_mapper.Map<IReadOnlyList<TeamDto>>(dbList));
            return Ok(response);
        }

        [HttpGet("{teamId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetTeamsResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(
            Summary = "Get details about a specific Team",
            Description = "Gets details about a specific Team",
            OperationId = "Teams.Get")
        ]
        public async Task<ActionResult<GetTeamsResponse>> GetTeam([FromRoute] int teamId, CancellationToken cancellationToken)
        {
            var response = new GetTeamsResponse
            {
                Team = _mapper.Map<TeamDto>(await _teamsRepository.GetByIdAsync(teamId, cancellationToken))
            };
            if (response.Team != null)
            {
                return Ok(response);
            } 
            else
            {
                return NotFound();
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateTeamResponse))]
        [SwaggerOperation(
            Summary = "Add a new Team to the database",
            Description = "Adds a new Team to the database, assigning a new id to him",
            OperationId = "Teams.Create")
]
        public async Task<ActionResult<CreateTeamResponse>> CreateTeam([FromBody] CreateTeamRequest createTeamRequest, CancellationToken cancellationToken)
        {
            var team = new Team()
            {
                Name = createTeamRequest.Name,
                FoundedIn = createTeamRequest.FoundedIn
            };
            await _teamsRepository.AddAsync(team, cancellationToken);
            var response = new CreateTeamResponse
            {
                Team = _mapper.Map<TeamDto>(team)
            };
            return Ok(response);
        }

        [HttpPost("{teamId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateTeamResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(
            Summary = "Update a specific Team",
            Description = "Updated a specific Team data",
            OperationId = "Teams.Update")
]
        public async Task<ActionResult<CreateTeamResponse>> UpdateTeam([FromRoute] int teamId, [FromBody] UpdateTeamRequest createTeamRequest, CancellationToken cancellationToken)
        {
            var team = await _teamsRepository.GetByIdAsync(teamId, cancellationToken);
            if (team != null)
            {
                team.Name = createTeamRequest.Name;
                team.FoundedIn = createTeamRequest.FoundedIn;

                await _teamsRepository.UpdateAsync(team, cancellationToken);

                var response = new CreateTeamResponse()
                {
                    Team = _mapper.Map<TeamDto>(team)
                };
                return Ok(response);
            } 
            else
            {
                return BadRequest();
            }           
        }


        [HttpDelete("{teamId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(
            Summary = "Delete a specific Team",
            Description = "Delete a team and makes all it's player free agents",
            OperationId = "Teams.Delete")
        ]
        public async Task<StatusCodeResult> DeleteTeam([FromRoute] int teamId, CancellationToken cancellationToken)
        {
            var team = await _teamsRepository.GetByIdWithRoosterAsync(teamId);
            if (team != null)
            {
                await _teamsRepository.DeleteAsync(team);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
