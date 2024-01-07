using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Einzel.Models;

namespace Einzel.Data
{
    public class UsuarioContext : IdentityDbContext<Usuario>
    {
        public UsuarioContext(DbContextOptions<UsuarioContext> opts) : base(opts)
        {
        }

        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Compra> Compras { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Compra>()
                .HasOne(c => c.EnderecoEntrega)
                .WithMany()
                .HasForeignKey(c => c.EnderecoId);

            modelBuilder.Entity<Compra>()
                .HasOne(c => c.Usuario)
                .WithMany(u => u.Compras)
                .HasForeignKey(c => c.UsuarioId)
                .IsRequired();

            modelBuilder.Entity<Endereco>()
                .HasOne(e => e.Usuario)
                .WithMany(u => u.Enderecos)
                .HasForeignKey(e => e.UsuarioId);
        }
    }
}