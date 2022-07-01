using Microsoft.EntityFrameworkCore;
using RegistrationControl.Models;

namespace RegistrationControl.Data
{
    public class RegistrationControlContext : DbContext
    {
        public RegistrationControlContext(DbContextOptions<RegistrationControlContext> options) : base(options) { }

        public DbSet<Registered> Registered { get; set; }
        public DbSet<Instructor> Instructor { get; set; }
        public DbSet<Live> Live { get; set; }
        public DbSet<Enrollment> Enrollment { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Live>()
                .Property(x => x.RegistrationFee)
                .HasColumnType("decimal(18,4)");
        }
    }
}