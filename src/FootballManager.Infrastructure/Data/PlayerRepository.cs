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
    public class PlayerRepository : EfRepository<Player>, IPlayerRepository
    {
        public PlayerRepository(FootballManagerContext dbContext) : base(dbContext)
        {
        }

        public Task<Player> GetByIdWithStatusAsync(int id)
        {
            return _dbContext.Players
                .Include(o => o.Statuses)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
