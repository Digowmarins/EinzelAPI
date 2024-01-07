using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Produto
{
    [Key]
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public string Composicao { get; set; }
    public double Preco { get; set; }
    public string Categoria { get; set; }
    public int Peso { get; set; }
    public int Altura { get; set; }
    public int Comprimento { get; set; }
    public int Largura { get; set; }
    public virtual ICollection<ImagemProduto> Imagens { get; set; }
    public virtual ICollection<VariacaoTamanho> Variacoes { get; set; }
}

public class VariacaoTamanho
{
    public int Id { get; set; }
    public string Tamanho { get; set; }
    public int Estoque { get; set; }
    public string Cor { get; set; }
    public int ProdutoId { get; set; }
    public virtual Produto Produto { get; set; } 
    public virtual ICollection<Medidas>? Medidas { get; set; }
}

public class Medidas
{
    public int Id { get; set; } 
    public string Regiao { get; set; }
    public double RegTam { get; set; }
    public int VariacaoTamanhoId { get; set; }
    public virtual VariacaoTamanho VariacaoTamanho { get; set; }
}


public class ImagemProduto
{
    public int Id { get; set; } 
    public byte[] DadosImagem { get; set; } 
    public int ProdutoId { get; set; } 
    public Produto Produto { get; set; } 
}