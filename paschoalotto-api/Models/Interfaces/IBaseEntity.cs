namespace paschoalotto_api.Models.Interfaces
{
    public interface IBaseEntity
    {
        int Id { get; set; }

        DateTimeOffset CreateAt { get; set; }

        DateTimeOffset? LastUpdateAt { get; set; }
    }
}
