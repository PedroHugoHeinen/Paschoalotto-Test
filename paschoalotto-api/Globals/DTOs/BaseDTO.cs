namespace paschoalotto_api.Globals.DTOs
{
    public class BaseDTO
    {
        public int Id { get; set; }

        public DateTimeOffset CreateAt { get; set; }

        public DateTimeOffset? LastUpdateAt { get; set; }
    }
}
