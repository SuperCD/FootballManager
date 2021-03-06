using AutoMapper;
using FootballManager.API.Dto;
using FootballManager.API.Requests;
using FootballManager.API.Responses;
using FootballManager.Domain.Entities;
using FootballManager.Domain.Exceptions;
using FootballManager.Domain.Interfaces;
using FootballManager.Domain.SeedWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FootballManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayersController : ControllerBase
    {

        private readonly ILogger<PlayersController> _logger;
        private readonly IMapper _mapper;
        private readonly IPlayerRepository _playersRepository;

        public PlayersController(IPlayerRepository playersRepository,
            IMapper mapper,
            ILogger<PlayersController> logger)
        {
            _logger = logger;
            _mapper = mapper;
            _playersRepository = playersRepository;
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
            var dbList = await _playersRepository.ListAsync(cancellationToken);
            response.Players.AddRange(_mapper.Map<IReadOnlyList<PlayerDto>>(dbList));
            return Ok(response);
        }

        [HttpGet("{playerId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetPlayersResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            Summary = "Get details about a specific player",
            Description = "Gets details about a specific player",
            OperationId = "players.Get")
        ]
        public async Task<ActionResult<GetPlayersResponse>> GetPlayer([FromRoute] int playerId, CancellationToken cancellationToken)
        {
            var response = new GetPlayersResponse
            {
                Player = _mapper.Map<PlayerDetailsDto>(await _playersRepository.GetByIdWithStatusAsync(playerId))
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
            var player = new Player()
            {
                Name = createPlayerRequest.Name,
                Surname = createPlayerRequest.Surname,
                Role = PlayerRole.GetByAcronym(createPlayerRequest.RoleAcronym)
            };
            await _playersRepository.AddAsync(player, cancellationToken);
            var response = new CreatePlayerResponse();
            response.Player = _mapper.Map<PlayerDto>(player);
            return Ok(response);
        }

        [HttpPost("{playerId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreatePlayerResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            Summary = "Update a specific player",
            Description = "Updated a specific player data",
            OperationId = "players.Update")
]
        public async Task<ActionResult<CreatePlayerResponse>> UpdatePlayer([FromRoute] int playerId, [FromBody] UpdatePlayerRequest createPlayerRequest, CancellationToken cancellationToken)
        {
            var player = await _playersRepository.GetByIdAsync(playerId, cancellationToken);
            if (player != null)
            {
                player.Name = createPlayerRequest.Name;
                player.Surname = createPlayerRequest.Surname;
                player.Role = PlayerRole.GetByAcronym(createPlayerRequest.RoleAcronym);

                await _playersRepository.UpdateAsync(player, cancellationToken);

                var response = new CreatePlayerResponse
                {
                    Player = _mapper.Map<PlayerDto>(player)
                };
                return Ok(response);
            } 
            else
            {
                return NotFound();
            }

            
        }

        [HttpDelete("{playerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            Summary = "Delete a specific player",
            Description = "Deletes a specific player from the database",
            OperationId = "players.Delete")
        ]
        public async Task<StatusCodeResult> DeletePlayer([FromRoute] int playerId, CancellationToken cancellationToken)
        {
            var player = await _playersRepository.GetByIdAsync(playerId, cancellationToken);

            if (player != null)
            {
                await _playersRepository.DeleteAsync(player);

                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{playerId}/status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            Summary = "gets a specific player status",
            Description = "Gets the list of statuses that affect a player",
            OperationId = "players.status.get")
        ]
        public async Task<ActionResult<List<string>>> GetPlayerStatus([FromRoute] int playerId, CancellationToken cancellationToken)
        {
            var player = await _playersRepository.GetByIdWithStatusAsync(playerId);

            if (player != null)
            {
                return Ok(_mapper.Map<List<string>>(player.Statuses));
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut("{playerId}/status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(
    Summary = "Adds a status to a player",
    Description = "Adds a specific status to the ones that affect the player",
    OperationId = "players.status.put")
]
        public async Task<ActionResult<List<string>>> AddPlayerStatus([FromRoute] int playerId, [FromBody] PlayerStatusRequest request, CancellationToken cancellationToken)
        {
            var player = await _playersRepository.GetByIdWithStatusAsync(playerId);

            if (player != null)
            {
                try
                {
                    player.ApplyStatus(Enumeration.GetById<PlayerStatus>(request.StatusId));
                }
                catch (Exception)
                {
                    return BadRequest($"Invalid Player Status {request.StatusId}");
                }
                
                await _playersRepository.UpdateAsync(player);
                return Ok(_mapper.Map<List<string>>(player.Statuses));
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{playerId}/status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(
            Summary = "Deletes a status from a player",
            Description = "Deletes a specific status from the ones that affect the player",
            OperationId = "players.status.delete")
]
        public async Task<ActionResult<List<string>>> DeletePlayerStatus([FromRoute] int playerId, [FromBody] PlayerStatusRequest request, CancellationToken cancellationToken)
        {
            var player = await _playersRepository.GetByIdWithStatusAsync(playerId);

            if (player != null)
            {
                try
                {
                    player.RemoveStatus(Enumeration.GetById<PlayerStatus>(request.StatusId));
                }
                catch (PlayerStatusNotPresentException)
                {
                    return BadRequest("Player Status not found on player");
                }
                catch (Exception)
                {
                    return BadRequest($"Invalid Player Status {request.StatusId}");
                }

                await _playersRepository.UpdateAsync(player);

                return Ok(_mapper.Map<List<string>>(player.Statuses));
            }
            else
            {
                return NotFound();
            }
        }
    }


}
