using FootballManager.Domain.Entities;
using FootballManager.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManager.Infrastructure.Data
{
    public class TeamRepository : EfRepository<Team>, ITeamRepository
    {
        public TeamRepository(FootballManagerContext dbContext) : base(dbContext)
        {
        }

        public Task<Team> GetByIdWithRoosterAsync(int id)
        {
            return _dbContext.Teams
                .Include(o => o.Rooster)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
