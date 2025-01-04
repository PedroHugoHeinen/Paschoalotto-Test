using paschoalotto_api.Models;

namespace paschoalotto_api.Services.Interfaces
{
    public interface IUserRandomService
    {
        Task<IEnumerable<User>> GetUsersRandomAsync(int quantity);
    }
}
