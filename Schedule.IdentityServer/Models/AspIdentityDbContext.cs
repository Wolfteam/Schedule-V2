using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Schedule.IdentityServer.Common;
using Schedule.IdentityServer.Interfaces;
using Schedule.IdentityServer.Models.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Schedule.Domain.Interfaces.Managers;

namespace Schedule.IdentityServer.Models
{
    public class AspIdentityDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, long>
    {
        private readonly IDefaultAppUserManager _userManager;

        public AspIdentityDbContext(
            DbContextOptions<AspIdentityDbContext> options,
            IDefaultAppUserManager appUserManager)
            : base(options)
        {
            _userManager = appUserManager;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(AspIdentityDbContext).Assembly);
            builder.HasDefaultSchema(AppConstants.AspiScheme);
        }

        public override int SaveChanges()
        {
            AddMissingValues();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AddMissingValues();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void AddMissingValues()
        {
            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);

            var now = DateTimeOffset.UtcNow;

            foreach (var entry in modifiedEntries)
            {
                if (!(entry.Entity is IBaseEntity))
                    continue;

                var entity = entry.Entity as IBaseEntity;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedAt = now;
                    entity.CreatedBy = _userManager.Username;
                }
                else
                {
                    entity.UpdatedAt = now;
                    entity.UpdatedBy = _userManager.Username;
                }
            }
        }
    }
}
