using Microsoft.EntityFrameworkCore;

namespace wwe.Models
{
    public class WweContext : DbContext
    {
        public WweContext(DbContextOptions options) : base(options)
        {
        }

        protected WweContext()
        {
        }

        public virtual DbSet<ComponentProcessing> ComponentProcessings { get; set; }

        public virtual DbSet<ProcessResponse> ProcessResponses { get; set; }

        public virtual DbSet<Payment> Payments { get; set; }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=SAYALI;Database=ReturnOrder;integrated security=true");
            }
        }
    }
}
