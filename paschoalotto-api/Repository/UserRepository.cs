using paschoalotto_api.Data;
using paschoalotto_api.Models;
using paschoalotto_api.Repository.Interfaces;

namespace paschoalotto_api.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) { }
    }
}
