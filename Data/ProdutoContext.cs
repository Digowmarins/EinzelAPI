using Microsoft.EntityFrameworkCore;
using Einzel.Models;

namespace Einzel.Data
{
    public class ProdutoContext : DbContext
    {
        public ProdutoContext(DbContextOptions<ProdutoContext> opts) : base(opts)
        {

        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<VariacaoTamanho> VariacoesTamanho { get; set; }
        public DbSet<Medidas> Medidas { get; set; }
        public DbSet<ImagemProduto> ImagensProdutos { get; set; } 
        public DbSet<Banner> Banners {  get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
          
            modelBuilder.Entity<Produto>()
                .HasKey(p => p.Id);
           
            modelBuilder.Entity<VariacaoTamanho>()
                .HasKey(v => v.Id);
           
            modelBuilder.Entity<Medidas>()
                .HasKey(m => m.Id);
          
            modelBuilder.Entity<ImagemProduto>()
                .HasKey(i => i.Id); 

            modelBuilder.Entity<Produto>()
                .HasMany(p => p.Variacoes);

            modelBuilder.Entity<Produto>()
                .HasMany(p => p.Imagens);

            // Relacionamento entre Produto e VariacaoTamanho
            modelBuilder.Entity<VariacaoTamanho>()
                .HasOne(v => v.Produto)
                .WithMany(p => p.Variacoes)
                .HasForeignKey(v => v.ProdutoId);

            // Relacionamento entre VariacaoTamanho e Medidas
            modelBuilder.Entity<Medidas>()
                .HasOne(m => m.VariacaoTamanho)
                .WithMany(v => v.Medidas)
                .HasForeignKey(m => m.VariacaoTamanhoId);

       
        }

    }
}