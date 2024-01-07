namespace Einzel.Data.Dtos
{
    public class ReadProdutoDto
    {
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
        public List<string> ImagensURL { get; set; } 
        public List<ReadVariacaoTamanhoDto> Variacoes { get; set; }
    }

    public class ReadVariacaoTamanhoDto
    {
        public string Tamanho { get; set; }
        public int Estoque { get; set; }
        public string Cor { get; set; }
        public List<ReadMedidasDto> Medidas { get; set; }
    }

    public class ReadMedidasDto
    {
        public string Regiao { get; set; }
        public double RegTam { get; set; }
    }

}

