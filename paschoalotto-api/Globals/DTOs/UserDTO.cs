namespace paschoalotto_api.Globals.DTOs
{
    public class UserDTO : BaseDTO
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? Gender { get; set; }

        public int Age { get; set; }

        public bool Status { get; set; }
    }
}
