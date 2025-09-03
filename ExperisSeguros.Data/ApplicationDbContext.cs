using ExperisSeguros.Data.Enums;
using ExperisSeguros.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace ExperisSeguros.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Poliza> Polizas { get; set; }
        public DbSet<TipoPoliza> TiposPoliza { get; set; }
        public DbSet<Pais> Paises { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configurar índice único para NumeroPoliza
            builder.Entity<Poliza>()
                .HasIndex(p => p.NumeroPoliza)
                .IsUnique();

            // Relación Poliza con Cliente
            builder.Entity<Poliza>()
                .HasOne(p => p.Cliente)
                .WithMany(u => u.PolizasComoCliente)
                .HasForeignKey(p => p.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            // Valores por defecto
            builder.Entity<Poliza>()
                .Property(p => p.Estado)
                .HasDefaultValue(EstadoPoliza.Cotizada);

            builder.Entity<Poliza>()
                .Property(p => p.FechaCreacion)
                .HasDefaultValueSql("GETDATE()");

            builder.Entity<Pais>().HasData(
                new Pais { Id = 1, Codigo = "MX", Nombre = "México" },
                new Pais { Id = 2, Codigo = "US", Nombre = "Estados Unidos" },
                new Pais { Id = 3, Codigo = "CA", Nombre = "Canadá" },
                new Pais { Id = 4, Codigo = "AR", Nombre = "Argentina" },
                new Pais { Id = 5, Codigo = "BR", Nombre = "Brasil" },
                new Pais { Id = 6, Codigo = "CL", Nombre = "Chile" },
                new Pais { Id = 7, Codigo = "CO", Nombre = "Colombia" },
                new Pais { Id = 8, Codigo = "PE", Nombre = "Perú" }
            );

            // Tipos de Póliza
            builder.Entity<TipoPoliza>().HasData(
                new TipoPoliza { Id = 1, Nombre = "Vida", Descripcion = "Seguro de vida" },
                new TipoPoliza { Id = 2, Nombre = "Auto", Descripcion = "Seguro automotriz" },
                new TipoPoliza { Id = 3, Nombre = "Hogar", Descripcion = "Seguro de hogar" },
                new TipoPoliza { Id = 4, Nombre = "Salud", Descripcion = "Seguro de salud" }
            );

            #region Usuarios y Roles

            // IDs para roles
            var adminRoleId = "a18be9c0-aa65-4af8-bd17-00bd9344e575";
            var brokerRoleId = "b18be9c0-aa65-4af8-bd17-00bd9344e576";
            var clienteRoleId = "c18be9c0-aa65-4af8-bd17-00bd9344e577";

            // IDs para usuarios
            var adminUserId = "a18be9c0-aa65-4af8-bd17-00bd9344e578";
            var brokerUserId = "b18be9c0-aa65-4af8-bd17-00bd9344e579";
            var clienteUserId = "c18be9c0-aa65-4af8-bd17-00bd9344e580";

            var hasher = new PasswordHasher<ApplicationUser>();

            // Crear roles
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = adminRoleId
                },
                new IdentityRole
                {
                    Id = brokerRoleId,
                    Name = "Broker",
                    NormalizedName = "BROKER",
                    ConcurrencyStamp = brokerRoleId
                },
                new IdentityRole
                {
                    Id = clienteRoleId,
                    Name = "Cliente",
                    NormalizedName = "CLIENTE",
                    ConcurrencyStamp = clienteRoleId
                }
            );

            builder.Entity<ApplicationUser>().HasData(
                // Admin
                new ApplicationUser
                {
                    Id = adminUserId,
                    UserName = "carlos.ramirez@experis.com",
                    NormalizedUserName = "CARLOS.RAMIREZ@EXPERIS.COM",
                    Email = "carlos.ramirez@experis.com",
                    NormalizedEmail = "CARLOS.RAMIREZ@EXPERIS.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Admin123!"),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    Nombre = "Carlos",
                    ApellidoPaterno = "Ramírez",
                    ApellidoMaterno = "Torres",
                    FechaNacimiento = DateTime.Today.AddYears(-40),
                    Genero = Genero.Hombre,
                    PaisId = 1,
                    PhoneNumber = "+5215512345670",
                    PhoneNumberConfirmed = true
                },

                // Broker
                new ApplicationUser
                {
                    Id = brokerUserId,
                    UserName = "andrea.mendoza@experis.com",
                    NormalizedUserName = "ANDREA.MENDOZA@EXPERIS.COM",
                    Email = "andrea.mendoza@experis.com",
                    NormalizedEmail = "ANDREA.MENDOZA@EXPERIS.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Broker123!"),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    Nombre = "Andrea",
                    ApellidoPaterno = "Mendoza",
                    ApellidoMaterno = "Hernández",
                    FechaNacimiento = DateTime.Today.AddYears(-33),
                    Genero = Genero.Mujer,
                    PaisId = 1, 
                    PhoneNumber = "+5215512345671",
                    PhoneNumberConfirmed = true
                },

                // Cliente
                new ApplicationUser
                {
                    Id = clienteUserId,
                    UserName = "fernando.lopez@gmail.com",
                    NormalizedUserName = "FERNANDO.LOPEZ@GMAIL.COM",
                    Email = "fernando.lopez@gmail.com",
                    NormalizedEmail = "FERNANDO.LOPEZ@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Cliente123!"),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    Nombre = "Fernando",
                    ApellidoPaterno = "López",
                    ApellidoMaterno = "García",
                    FechaNacimiento = DateTime.Today.AddYears(-27),
                    Genero = Genero.Hombre,
                    PaisId = 1,
                    PhoneNumber = "+5215512345672",
                    PhoneNumberConfirmed = true
                }
            );

            // Asignar roles a usuarios
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleId,
                    UserId = adminUserId
                },
                new IdentityUserRole<string>
                {
                    RoleId = brokerRoleId,
                    UserId = brokerUserId
                },
                new IdentityUserRole<string>
                {
                    RoleId = clienteRoleId,
                    UserId = clienteUserId
                }
            );

            #endregion
        }
    }
}
