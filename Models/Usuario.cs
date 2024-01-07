using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Einzel.Models
{
    public class Usuario : IdentityUser
    {
        public string NomeCompleto { get; set; }
        public DateTime DataCriacao { get; set; }
        public virtual ICollection<Compra> Compras { get; set; }
        public virtual ICollection<Endereco> Enderecos { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9\s]*$", ErrorMessage = "O campo UserName pode conter apenas letras, números e espaços.")]
        public override string UserName
        {
            get => base.UserName;
            set => base.UserName = value;
        }
        public string Role { get; set; }

        public string? ResetPasswordToken { get; set; }
        public DateTime? ResetPasswordTokenExpiration { get; set; }

    }
}
