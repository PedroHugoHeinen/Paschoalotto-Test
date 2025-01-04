using paschoalotto_api.Models.Interfaces;

namespace paschoalotto_api.Models
{
    public class BaseEntity : IBaseEntity
    {
        public int Id { get; set; }

        public DateTimeOffset CreateAt { get; set; }

        public DateTimeOffset? LastUpdateAt { get; set; }
    }
}
