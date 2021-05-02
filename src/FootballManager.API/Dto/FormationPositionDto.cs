using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballManager.API.Dto
{
    public class FormationPositionDto
    {
        public int PositionNo { get; set; }
        public string RoleAcronym { get; set; }
        public string PlayerId { get; set; }
        public string PlayerFullName { get; set; }
    }
}
