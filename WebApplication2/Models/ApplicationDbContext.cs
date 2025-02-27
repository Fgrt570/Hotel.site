using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Определите наборы сущностей (DbSet)

        public DbSet<MRooms> Rooms { get; set; }//комнаты
        public DbSet<Bookingg> Bookinggg { get; set; }//бронь
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<MRooms>()
           .Property(p => p.Price)
           .HasColumnType("decimal(18, 0)");

        }
    }


}
