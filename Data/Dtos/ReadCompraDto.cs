using System.ComponentModel.DataAnnotations;

namespace Einzel.Data.Dtos
{
    public class ReadCompraDto
    {

        public int Id { get; set; }
        public DateTime DataCompra { get; set; }
        public decimal Total { get; set; }
        public string MetodoPagamento { get; set; }
        public string? StatusCompra { get; set; }
        public string? CodigoRastreio { get; set; }
        public string UsuarioId { get; set; }
        public int EnderecoId { get; set; }
        public ReadEnderecoDto EnderecoEntrega { get; set; }
        public int ProdutoId { get; set; }
        public string? ProdutoNome { get; set; }
    }
}