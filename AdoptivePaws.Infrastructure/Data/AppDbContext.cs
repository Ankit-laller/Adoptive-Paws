using Microsoft.EntityFrameworkCore;
using AdoptivePaws.Core.Entities;

namespace AdoptivePaws.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) :DbContext(options)
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<PetEntity> Pets { get; set; }
    }
}
