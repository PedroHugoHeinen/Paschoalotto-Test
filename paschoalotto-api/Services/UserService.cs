using paschoalotto_api.Models;
using paschoalotto_api.Services.Interfaces;
using paschoalotto_api.Globals.DTOs;
using paschoalotto_api.Repository.Interfaces;
using AutoMapper;

namespace paschoalotto_api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IUserRandomService userRandomService;
        private readonly IMapper mapper;

        public UserService(IUserRepository userRepository,
               IUserRandomService userRandomService,
               IMapper mapper)
        {
            this.userRepository = userRepository;
            this.userRandomService = userRandomService;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<UserDTO>> GetAllAsync()
        {
            var users = await userRepository.GetAllAsync();

            return this.mapper.Map<IEnumerable<UserDTO>>(users).OrderBy(x => x.Id);
        }

        public async Task<UserDTO> GetByIdAsync(int id)
        {
            var user = await this.userRepository.GetByIdAsync(id);

            if (user == default)
            {
                return new UserDTO();
            }

            return this.mapper.Map<UserDTO>(user);
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
                    CreateAt = DateTimeOffset.Now.ToUniversalTime(),
                    LastUpdateAt = null,
                });

            return this.mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> UpdateAsync(UserDTO userDTO)
        {
            var userToUpdate = this.mapper.Map<User>(userDTO);

            userToUpdate.CreateAt = userToUpdate.CreateAt.ToUniversalTime();
            userToUpdate.LastUpdateAt = DateTimeOffset.Now.ToUniversalTime();

            var user = await this.userRepository.UpdateAsync(userToUpdate);

            return this.mapper.Map<UserDTO>(user);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await this.userRepository.DeleteAsync(id); 
        }

        public async Task<IEnumerable<UserDTO>> InsertRandomAsync()
        {
            var users = await this.userRandomService.GetUsersRandomAsync(10);

            if (users?.Any() != true)
            {
                return Enumerable.Empty<UserDTO>();
            }

            var insertedUsers = new List<User>();

            foreach (var user in users)
            {
                insertedUsers.Add(await this.userRepository.InsertAsync(user));
            }

            return this.mapper.Map<IEnumerable<UserDTO>>(insertedUsers);
        }
    }
}