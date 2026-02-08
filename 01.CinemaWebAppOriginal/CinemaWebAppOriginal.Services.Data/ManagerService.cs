using CinemaWebAppOriginal.Data.Models;
using CinemaWebAppOriginal.Infrastructure.Repositories.Contracts;
using CinemaWebAppOriginal.Services.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CinemaWebAppOriginal.Services.Data
{
    public class ManagerService : IManagerService
    {
        private readonly IRepository<Manager, Guid> managerRepository;


        public ManagerService(IRepository<Manager, Guid> _managerRepository)
        {
            this.managerRepository = _managerRepository;
        }


        // check if user is a manager
        public async Task<bool> IsUserAManager(string userid)
        {
            bool result = await this.managerRepository.GetAllAttached().AnyAsync(m => m.UserId == userid);

            return result;
        }
    }
}
