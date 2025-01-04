using System.Text.Json;
using paschoalotto_api.Models;
using paschoalotto_api.Services.Interfaces;

namespace paschoalotto_api.Services
{
    public class UserRandomService : IUserRandomService
    {
        private readonly HttpClient httpClient;

        public UserRandomService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<User>> GetUsersRandomAsync(int quantity)
        {
            var response = await this.httpClient.GetStringAsync($"https://randomuser.me/api/?results={quantity}");

            var jsonDocument = JsonDocument.Parse(response);

            var users = new List<User>();

            foreach (var userJson in jsonDocument.RootElement.GetProperty("results").EnumerateArray())
            {
                users.Add(new User
                {
                    Id = 0,
                    FirstName = userJson.GetProperty("name").GetProperty("first").GetString(),
                    LastName = userJson.GetProperty("name").GetProperty("last").GetString(),
                    Email = userJson.GetProperty("email").GetString(),
                    Phone = userJson.GetProperty("phone").GetString(),
                    Gender = userJson.GetProperty("gender").GetString(),
                    Age = userJson.GetProperty("dob").GetProperty("age").GetInt32(),
                    Status = true,
                    CreateAt = DateTimeOffset.Now.ToUniversalTime(),
                    LastUpdateAt = null,
                });
            }

            return users;
        }
    }
}