using FootballManager.Domain.Entities;
using FootballManager.Domain.SeedWork;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FootballManager.Domain.Interfaces
{
    public interface ITeamRepository : IAsyncRepository<Team>
    {
        Task<Team> GetByIdWithRoosterAsync(int id);
        Task<Team> GetByIdWithFormationAsync(int id);
    }
}
