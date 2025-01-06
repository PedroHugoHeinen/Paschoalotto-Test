using paschoalotto_api.Models;
using paschoalotto_api.Services.Interfaces;
using paschoalotto_api.Globals.DTOs;
using paschoalotto_api.Repository.Interfaces;
using AutoMapper;
using OfficeOpenXml;
using paschoalotto_api.Models.Interfaces;

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

        public async Task<byte[]> GenerateReportAsync()
        {
            var users = await this.GetAllAsync();

            if (users?.Any() != true)
            {
                return [];
            }

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Paschoalotto");

                worksheet.Cells[1, 1].Value = "Paschoalotto";
                worksheet.Cells[1, 1].Style.Font.Bold = true;
                worksheet.Cells[1, 1].Style.Font.Size = 20;
                worksheet.SelectedRange[1, 1, 1, 3].Merge = true;

                worksheet.Cells[3, 1].Value = "Id";
                worksheet.Cells[3, 2].Value = "Primeiro Nome";
                worksheet.Cells[3, 3].Value = "Último Nome";
                worksheet.Cells[3, 4].Value = "E-mail";
                worksheet.Cells[3, 5].Value = "Contato";
                worksheet.Cells[3, 6].Value = "Gênero";
                worksheet.Cells[3, 7].Value = "Idade";
                worksheet.Cells[3, 8].Value = "Status";
                worksheet.Cells[3, 9].Value = "Criado em";
                worksheet.Cells[3, 10].Value = "Última atualização em";

                worksheet.SelectedRange[3, 1, 3, 10].Style.Font.Bold = true;

                int row = 4;

                foreach (var user in users)
                {
                    worksheet.Cells[row, 1].Value = user.Id;
                    worksheet.Cells[row, 2].Value = user.FirstName;
                    worksheet.Cells[row, 3].Value = user.LastName;
                    worksheet.Cells[row, 4].Value = user.Email;
                    worksheet.Cells[row, 5].Value = user.Phone;
                    worksheet.Cells[row, 6].Value = user.Gender;
                    worksheet.Cells[row, 7].Value = user.Age;
                    worksheet.Cells[row, 8].Value = (user.Status ? "Ativo" : "Inativo");
                    worksheet.Cells[row, 9].Value = user.CreateAt.ToString("dd/MM/yyyy HH:mm:ss");
                    worksheet.Cells[row, 10].Value = user.LastUpdateAt?.ToString("dd/MM/yyyy HH:mm:ss") ?? "";

                    row++;
                }

                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                return package.GetAsByteArray();
            }
        }
    }
}