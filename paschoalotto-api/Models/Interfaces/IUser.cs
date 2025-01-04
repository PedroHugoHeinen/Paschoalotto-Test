namespace paschoalotto_api.Models.Interfaces
{
    public interface IUser : IBaseEntity
    {
        string FirstName { get; set; }
        
        string LastName { get; set; }
        
        string Email { get; set; }

        string Phone { get; set; }

        string Gender { get; set; }

        int Age { get; set; }

        bool Status { get; set; }
    }
}
