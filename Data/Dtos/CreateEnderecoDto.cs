using System.ComponentModel.DataAnnotations;

namespace Einzel.Data.Dtos
{
    public class CreateEnderecoDto
    {
        public string Estado { get; set; }

        public string Cidade { get; set; }

        public string Bairro { get; set; }

        public string Complemento { get; set; }

        public int Numero { get; set; }

        public string Rua { get; set; }

        public string Cep { get; set; }

        public string Usuarioid { get; set; }

    }
}
