using paschoalotto_api.Models.Interfaces;

namespace paschoalotto_api.Models
{
    public class User : BaseEntity, IUser
    {
        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public required string Email { get; set; }

        public required string Phone { get; set; }

        public required string Gender { get; set; }

        public int Age { get; set; }

        public bool Status { get; set; }
    }
}
