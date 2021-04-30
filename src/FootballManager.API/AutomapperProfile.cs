using AutoMapper;
using FootballManager.API.Controllers.Players;
using FootballManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballManager.API
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Player, PlayerDto>();
        }
    }
}
