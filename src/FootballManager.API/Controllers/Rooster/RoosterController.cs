﻿using AutoMapper;
using FootballManager.API.Controllers.Players;
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
    public class RoosterController : ControllerBase
    {

        private readonly ILogger<RoosterController> _logger;
        private readonly IAsyncRepository<Player> _playersRepository;
        private readonly ITeamRepository _teamsRepository;
        private readonly IMapper _mapper;

        public RoosterController(IAsyncRepository<Player> playersRepository,
            ITeamRepository teamsRepository,
            IMapper mapper,
            ILogger<RoosterController> logger)
        {
            _logger = logger;
            _playersRepository = playersRepository;
            _teamsRepository = teamsRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ListRoosterResponse))]
        [SwaggerOperation(
            Summary = "List all Rooster",
            Description = "Gets the list of Rooster in the whole database",
            OperationId = "Rooster.List")
        ]
        public async Task<ActionResult<ListRoosterResponse>> ListRooster([FromRoute] int teamId, CancellationToken cancellationToken)
        {
            var team = await _teamsRepository.GetByIdWithRoosterAsync(teamId);

            var response = new ListRoosterResponse();
            {
                response.TeamId = teamId;
                response.Players.AddRange(_mapper.Map<List<PlayerDto>>(team.Rooster));
            }
            return Ok(response);
        }

        [HttpPut("{playerId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AddPlayerToRoosterResponse))]
        [SwaggerOperation(
            Summary = "Add Player to the rooster",
            Description = "Adds an existing player from the database to the team rooster",
            OperationId = "Rooster.Add")
        ]
        public async Task<ActionResult<AddPlayerToRoosterResponse>> AddPlayerToRooster([FromRoute] int teamId, [FromRoute] int playerId, CancellationToken cancellationToken)
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
