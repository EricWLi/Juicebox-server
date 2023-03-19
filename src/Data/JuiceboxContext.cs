using Microsoft.EntityFrameworkCore;
using JuiceboxServer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace JuiceboxServer.Data
{
    public class JuiceboxContext : IdentityDbContext<User>
    {
        public JuiceboxContext(DbContextOptions<JuiceboxContext> options) : base(options)
        {
        }

        public DbSet<Party> Parties { get; set; } = null!;
        public DbSet<QueueItem> QueueItems { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Add models here

            // Optional relationship between Party and User to avoid multiple cascading paths
            builder.Entity<Party>()
                .HasOne(party => party.Host)
                .WithMany(user => user.HostedParties)
                .IsRequired(false);

            builder.Entity<Party>()
                .HasMany(party => party.Members)
                .WithMany(user => user.JoinedParties);

            builder.Entity<QueueItem>()
                .HasOne(item => item.Party)
                .WithMany(party => party.Queue);
        }
    }
}