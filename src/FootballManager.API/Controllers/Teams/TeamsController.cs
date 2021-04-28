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
    public class TeamsController : ControllerBase
    {

        private readonly ILogger<TeamsController> _logger;
        private readonly List<TeamDto> testTeamDB;

        public TeamsController(ILogger<TeamsController> logger)
        {
            _logger = logger;
            int id = 1;
            Random r = new Random();
            // TEST
            testTeamDB = Enumerable.Range(1, 5).Select(index => new TeamDto()
            {
                Id = id++,
                Name = $"Real {id}",
                FoundedIn = DateTime.Now - TimeSpan.FromDays(r.Next(2000))
            }).ToList();
            //TEST
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
            response.Teams.AddRange(testTeamDB);
            return Ok(response);
        }

        [HttpGet("{TeamId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetTeamsResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(
            Summary = "Get details about a specific Team",
            Description = "Gets details about a specific Team",
            OperationId = "Teams.Get")
        ]
        public async Task<ActionResult<GetTeamsResponse>> GetTeam([FromRoute] int TeamId, CancellationToken cancellationToken)
        {
            var response = new GetTeamsResponse
            {
                Team = testTeamDB.SingleOrDefault(p => p.Id == TeamId)
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
            var Team = new TeamDto()
            {
                Id = testTeamDB.Count,
                Name = createTeamRequest.Name,
                FoundedIn = createTeamRequest.FoundedIn
            };
            testTeamDB.Add(Team);
            var response = new CreateTeamResponse();
            response.Team = Team;
            return Ok(response);
        }

        [HttpPost("{TeamId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateTeamResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(
            Summary = "Update a specific Team",
            Description = "Updated a specific Team data",
            OperationId = "Teams.Update")
]
        public async Task<ActionResult<CreateTeamResponse>> UpdateTeam([FromRoute] int TeamId, [FromBody] UpdateTeamRequest createTeamRequest, CancellationToken cancellationToken)
        {
            var Team = testTeamDB.SingleOrDefault(x => x.Id == TeamId);
            if (Team != null)
            {
                Team.Name = createTeamRequest.Name;
                Team.FoundedIn = createTeamRequest.FoundedIn;

                var response = new CreateTeamResponse
                {
                    Team = Team
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
