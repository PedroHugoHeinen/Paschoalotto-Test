using paschoalotto_api.Data;
using paschoalotto_api.Models;
using Microsoft.EntityFrameworkCore;
using paschoalotto_api.Services.Interfaces;
using paschoalotto_api.Globals.DTOs;
using paschoalotto_api.Repository.Interfaces;

namespace paschoalotto_api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<IEnumerable<UserDTO>> GetAllAsync()
        {
            var users = await userRepository.GetAllAsync();

            return users.Select(user => new UserDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone,
                Gender = user.Gender,
                Age = user.Age,
                Status = user.Status,
                CreateAt = user.CreateAt,
                LastUpdateAt = user.LastUpdateAt,
            });
        }

        public async Task<UserDTO> GetByIdAsync(int id)
        {
            var user = await this.userRepository.GetByIdAsync(id);

            if (user == default)
            {
                return new UserDTO();
            }

            return new UserDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone,
                Gender = user.Gender,
                Age = user.Age,
                Status = user.Status,
                CreateAt = user.CreateAt,
                LastUpdateAt = user.LastUpdateAt,
            };
        }

        public async Task<UserDTO> InsertAsync(UserDTO userDTO)
        {
            var user = await this.userRepository.InsertAsync(
                new User
                {
                    Id = 0,
                    FirstName = userDTO.FirstName,
                    LastName = userDTO.LastName,
                    Email = userDTO.Email,
                    Phone = userDTO.Phone,
                    Gender = userDTO.Gender,
                    Age = userDTO.Age,
                    Status = userDTO.Status,
                    CreateAt = DateTimeOffset.Now,
                    LastUpdateAt = null,
                });

            userDTO.Id = user.Id;
            return userDTO;
        }

        public async Task<UserDTO> UpdateAsync(UserDTO userDTO)
        {
            var user = await this.userRepository.GetByIdAsync(userDTO.Id);

            if (user == null)
            {
                return new UserDTO();
            }

            var userToUpdate = new User
            {
                Id = user.Id,
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                Email = userDTO.Email,
                Phone = userDTO.Phone,
                Gender = userDTO.Gender,
                Age = userDTO.Age,
                Status = userDTO.Status,
                CreateAt = user.CreateAt,
                LastUpdateAt = DateTimeOffset.Now,
            };

            await this.userRepository.UpdateAsync(userToUpdate);

            return new UserDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone,
                Gender = user.Gender,
                Age = user.Age,
                Status = user.Status,
                CreateAt = user.CreateAt,
                LastUpdateAt = user.LastUpdateAt,
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await this.userRepository.GetByIdAsync(id);

            if (user == default)
            {
                return false;
            }

            await this.DeleteAsync(user.Id);

            return true;
        }
    }
}
