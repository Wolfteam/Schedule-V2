using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Schedule.Domain.Entities;
using Schedule.Infrastructure.Persistence;
using System;

namespace Schedule.Api.IntegrationTests.Config
{
    public static class DbConfig
    {
        public static volatile bool IsDbCreated;
        private static readonly object Lock = new object();

        public static void Init(IServiceProvider scopedServices)
        {
            lock (Lock)
            {
                if (IsDbCreated)
                    return;

                var dbContext = scopedServices.GetRequiredService<AppDbContext>();
                dbContext.Database.EnsureDeleted();
                dbContext.Database.Migrate();

                dbContext.Schools.Add(new School
                {
                    Name = "La santisima trinidad",
                    Address = "Petare, cercal del colegio 23 de enero"
                });

                dbContext.SaveChangesAsync().GetAwaiter().GetResult();

                IsDbCreated = true;
            }
        }
    }
}
