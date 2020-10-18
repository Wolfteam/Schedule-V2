using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Schedule.Domain.Enums;
using Schedule.IdentityServer.Models;
using Schedule.IdentityServer.Models.Entities;
using Schedule.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Schedule.IdentityServer.Common.Extensions
{
    public static class IdentityAppBuilderExtensions
    {
        public static async Task ApplyIdentityMigrations(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                .CreateScope();
            await serviceScope.ServiceProvider.GetService<AspIdentityDbContext>().Database.MigrateAsync();
        }

        public static async Task SeedUsers(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            var userMgr = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var users = new List<ApplicationUser>
            {
                new ApplicationUser
                {
                    UserName = "unexpolcm",
                    Email = "unexpolcm@edu.com",
                    EmailConfirmed = true,
                    CreatedAt = DateTimeOffset.UtcNow,
                    FullName = "UnexpoLCM",
                    CreatedBy = "system"
                },
                new ApplicationUser
                {
                    UserName = "wolfteam20",
                    Email = "mimo4325@gmail.com.com",
                    EmailConfirmed = true,
                    CreatedAt = DateTimeOffset.UtcNow,
                    FullName = "Efrain Bastidas",
                    CreatedBy = "system"
                },
            };

            foreach (var user in users)
            {
                var result = await userMgr.CreateAsync(user, "sistemas20");
                if (!result.Succeeded)
                    throw new Exception("User couldn't be created");
                await userMgr.AddClaimsAsync(user, new[]
                {
                    new Claim(Shared.AppConstants.SchoolClaim, "1", ClaimValueTypes.Integer32),
                    new Claim(Shared.AppConstants.SchedulePermissionsClaim, SchedulePermissionType.All.GetPermissionStringValue(), ClaimValueTypes.Integer32),
                });
            }
        }
    }
}
