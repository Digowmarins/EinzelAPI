namespace Einzel.Data.Dtos
{
    public class CreateCompraDto
    {
        public decimal Total { get; set; }
        public DateTime Datacompra {  get; set; } = DateTime.Now;
        public string MetodoPagamento { get; set; }
        public string UsuarioId { get; set; }
        public int ProdutoId { get; set; }
        public int EnderecoId { get; set; }
    }
}
