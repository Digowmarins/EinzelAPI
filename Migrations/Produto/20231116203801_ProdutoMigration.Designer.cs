﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Einzel.Data;

#nullable disable

namespace EinzelAPI.Migrations.Produto
{
    [DbContext(typeof(ProdutoContext))]
    [Migration("20231116203801_ProdutoMigration")]
    partial class ProdutoMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ImagemProduto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<byte[]>("DadosImagem")
                        .IsRequired()
                        .HasColumnType("longblob");

                    b.Property<int>("ProdutoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProdutoId");

                    b.ToTable("ImagensProdutos");
                });

            modelBuilder.Entity("Medidas", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double>("RegTam")
                        .HasColumnType("double");

                    b.Property<string>("Regiao")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("VariacaoTamanhoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("VariacaoTamanhoId");

                    b.ToTable("Medidas");
                });

            modelBuilder.Entity("Produto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Altura")
                        .HasColumnType("int");

                    b.Property<string>("Categoria")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Comprimento")
                        .HasColumnType("int");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Largura")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Peso")
                        .HasColumnType("int");

                    b.Property<double>("Preco")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.ToTable("Produtos");
                });

            modelBuilder.Entity("VariacaoTamanho", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Cor")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Estoque")
                        .HasColumnType("int");

                    b.Property<int>("ProdutoId")
                        .HasColumnType("int");

                    b.Property<string>("Tamanho")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ProdutoId");

                    b.ToTable("VariacoesTamanho");
                });

            modelBuilder.Entity("ImagemProduto", b =>
                {
                    b.HasOne("Produto", "Produto")
                        .WithMany("Imagens")
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Produto");
                });

            modelBuilder.Entity("Medidas", b =>
                {
                    b.HasOne("VariacaoTamanho", "VariacaoTamanho")
                        .WithMany("Medidas")
                        .HasForeignKey("VariacaoTamanhoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("VariacaoTamanho");
                });

            modelBuilder.Entity("VariacaoTamanho", b =>
                {
                    b.HasOne("Produto", "Produto")
                        .WithMany("Variacoes")
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Produto");
                });

            modelBuilder.Entity("Produto", b =>
                {
                    b.Navigation("Imagens");

                    b.Navigation("Variacoes");
                });

            modelBuilder.Entity("VariacaoTamanho", b =>
                {
                    b.Navigation("Medidas");
                });
#pragma warning restore 612, 618
        }
    }
}
