using Microsoft.AspNetCore.Identity;
using Schedule.Domain.Enums;
using Schedule.IdentityServer.Interfaces;
using System;

namespace Schedule.IdentityServer.Models.Entities
{
    public class ApplicationUser : IdentityUser<long>, IBaseEntity
    {
        public string FullName { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public UserStatusType Status { get; set; }
    }
}
