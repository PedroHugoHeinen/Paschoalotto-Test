using paschoalotto_api.Globals.DTOs;

namespace paschoalotto_api.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAllAsync();

        Task<UserDTO> GetByIdAsync(int id);

        Task<UserDTO> InsertAsync(UserDTO userDTO);
        
        Task<UserDTO> UpdateAsync(UserDTO userDTO);
        
        Task<bool> DeleteAsync(int id);

        Task<IEnumerable<UserDTO>> GetRandomAsync();
    }
}