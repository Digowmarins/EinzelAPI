using System.ComponentModel.DataAnnotations;

namespace Einzel.Data.Dtos
{
    public class CreateUsuarioDto
    {
        [Required]
        public string NomeCompleto { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        //[Phone]
        //public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "As senhas não coincidem.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
    }
}
