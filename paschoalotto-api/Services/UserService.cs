using paschoalotto_api.Data;
using paschoalotto_api.Models;
using Microsoft.EntityFrameworkCore;
using paschoalotto_api.Services.Interfaces;
using paschoalotto_api.Globals.DTOs;

namespace paschoalotto_api.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext applicationDbContext;

        public UserService(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<UserDTO>> GetAllAsync()
        {
            return await this.applicationDbContext.User
                .Select(user => new UserDTO
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
                })
                .ToListAsync();
        }

        public async Task<UserDTO> GetByIdAsync(int id)
        {
            var user = await this.applicationDbContext.User.FindAsync(id);
            
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
            var user = new User
            {
                Id = 0,
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                Email = userDTO.Email,
                Phone= userDTO.Phone,
                Gender = userDTO.Gender,
                Age = userDTO.Age,
                Status = userDTO.Status,
                CreateAt = DateTimeOffset.Now,
                LastUpdateAt = null,
            };

            this.applicationDbContext.User.Add(user);
            await this.applicationDbContext.SaveChangesAsync();

            userDTO.Id = user.Id;
            return userDTO;
        }

        public async Task<UserDTO> UpdateAsync(UserDTO userDTO)
        {
            var user = await this.applicationDbContext.User.FindAsync(userDTO.Id);

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

            this.applicationDbContext.Entry(user).State = EntityState.Modified;
            await this.applicationDbContext.SaveChangesAsync();

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
            var user = await this.applicationDbContext.User.FindAsync(id);

            if (user == default)
            {
                return false;
            }

            this.applicationDbContext.User.Remove(user);
            await this.applicationDbContext.SaveChangesAsync();

            return true;
        }
    }
}
