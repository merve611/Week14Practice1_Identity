using Microsoft.EntityFrameworkCore;
using Week14Practice1_Identity.Entities;

namespace Week14Practice1_Identity.Context
{
    public class CustomIdentityDbContext : DbContext
    {
        public CustomIdentityDbContext(DbContextOptions<CustomIdentityDbContext> options) : base(options) 
        {
            
        }
        public DbSet<UserEntity> Users => Set<UserEntity>();


    }
}
