namespace Einzel.Data.Dtos
{
    public class ReadUsuarioDto
    {
        public string Id { get; set; }
        public string NomeCompleto { get; set; }
        public string NumeroTelefone { get; set; }
        public string Email { get; set; }
        public List<ReadEnderecoDto> Enderecos { get; set; }
        public List<ReadCompraDto> Compras { get; set; }

    }
}
