using FootballManager.Domain.Entities;
using FootballManager.Domain.SeedWork;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FootballManager.Domain.Interfaces
{
    public interface IPlayerRepository : IAsyncRepository<Player>
    {
        Task<Player> GetByIdWithStatusAsync(int id);
    }
}
