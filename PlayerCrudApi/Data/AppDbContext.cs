using Microsoft.EntityFrameworkCore;
using PlayerCrudApi.Players.Model;

namespace PlayerCrudApi.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Player> Players { get; set; }
    }
}
