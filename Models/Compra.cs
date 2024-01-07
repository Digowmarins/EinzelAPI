using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Einzel.Models
{
    public class Compra
    {
        [Key]
        public int Id { get; set; }
        public DateTime DataCompra { get; set; }
        public decimal Total { get; set; }
        public string MetodoPagamento { get; set; }
        public string? StatusCompra { get; set; }
        public string? CodigoRastreio { get; set; }
        [MaxLength(36)]
        public string UsuarioId { get; set; }
        [ForeignKey(nameof(UsuarioId))]
        public virtual Usuario Usuario { get; set; }
        public int EnderecoId { get; set; }
        [ForeignKey(nameof(EnderecoId))]
        public virtual Endereco EnderecoEntrega { get; set; }
        public int produtoId { get; set; }
        public string? produtoNome {  get; set; }
    }
}
