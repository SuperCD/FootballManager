using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballManager.API.Controllers.Players
{
    /// <summary>
    /// DTO class to avoid overexposing internal model data to the API
    /// </summary>
    public class PlayerDto
    {
        public int Id { get; set; }
        public string Name {get; set;}
        public string Surname { get; set; }
        public string RoleAcronym { get; internal set; }
        public int CurrentTeamId { get; internal set; }
    }
}
