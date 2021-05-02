using AutoMapper;
using FootballManager.API.Dto;
using FootballManager.Domain.Entities;

namespace FootballManager.API
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Player, PlayerDto>();
            CreateMap<Player, PlayerDetailsDto>();
            CreateMap<ActivePlayerStatus, string>().ConvertUsing(s => s.Status.Name);

            CreateMap<Team, TeamDto>();

            CreateMap<FormationPostition, FormationPositionDto>();
        }
    }
}
