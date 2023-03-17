using Microsoft.EntityFrameworkCore;
using JuiceboxServer.Models;

namespace JuiceboxServer.Data
{
    public class JuiceboxContext : DbContext
    {
        public JuiceboxContext(DbContextOptions<JuiceboxContext> options) : base(options)
        {}

        public DbSet<User> User { get; set; } = null!;
        public DbSet<Party> Party { get; set; } = null!;
    }
}