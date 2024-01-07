using System.ComponentModel.DataAnnotations;

namespace Einzel.Models
{
    public class Banner
    {
        [Key]
        public int id { get; set; }
        public byte[] DadosImagem { get; set; }
    }
}
