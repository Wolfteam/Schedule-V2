using System;

namespace Schedule.IdentityServer.Interfaces
{
    public interface IBaseEntity
    {
        long Id { get; set; }
        string CreatedBy { get; set; }
        DateTimeOffset CreatedAt { get; set; }

        string UpdatedBy { get; set; }
        DateTimeOffset? UpdatedAt { get; set; }
    }
}
