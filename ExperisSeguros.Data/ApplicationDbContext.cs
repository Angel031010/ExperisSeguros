using ExperisSeguros.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExperisSeguros.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Policy> Policies { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<PolicyType> PolicyTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Policy>()
                .HasIndex(p => p.NumeroPoliza)
                .IsUnique();

            builder.Entity<Policy>()
                .HasOne(p => p.Client)
                .WithMany(u => u.Policies)
                .HasForeignKey(p => p.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Policy>()
                .HasOne(p => p.Broker)
                .WithMany()
                .HasForeignKey(p => p.BrokerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<PolicyType>().HasData(
                new PolicyType { Id = 1, Nombre = "Vida" },
                new PolicyType { Id = 2, Nombre = "Auto" },
                new PolicyType { Id = 3, Nombre = "Hogar" },
                new PolicyType { Id = 4, Nombre = "Salud" }
            );

            builder.Entity<Country>().HasData(
                new Country { Id = 1, Nombre = "México" },
                new Country { Id = 2, Nombre = "Estados Unidos" },
                new Country { Id = 3, Nombre = "Canadá" },
                new Country { Id = 4, Nombre = "Argentina" },
                new Country { Id = 5, Nombre = "Brasil" },
                new Country { Id = 6, Nombre = "Chile" },
                new Country { Id = 7, Nombre = "Colombia" },
                new Country { Id = 8, Nombre = "Perú" },
                new Country { Id = 9, Nombre = "Venezuela" },
                new Country { Id = 10, Nombre = "Ecuador" },
                new Country { Id = 11, Nombre = "Bolivia" },
                new Country { Id = 12, Nombre = "Paraguay" },
                new Country { Id = 13, Nombre = "Uruguay" },
                new Country { Id = 14, Nombre = "Guatemala" },
                new Country { Id = 15, Nombre = "Costa Rica" },
                new Country { Id = 16, Nombre = "Panamá" },
                new Country { Id = 17, Nombre = "República Dominicana" },
                new Country { Id = 18, Nombre = "Honduras" },
                new Country { Id = 19, Nombre = "El Salvador" },
                new Country { Id = 20, Nombre = "Nicaragua" },
                new Country { Id = 21, Nombre = "Cuba" },
                new Country { Id = 22, Nombre = "Puerto Rico" }
            );
        }
    }
}
