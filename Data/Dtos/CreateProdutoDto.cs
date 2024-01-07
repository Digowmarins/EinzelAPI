using System.ComponentModel.DataAnnotations;
namespace Einzel.Data.Dtos
{
    public class CreateProdutoDto
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
        public ICollection<CreateVariacaoTamanhoDto> Variacoes { get; set; }
    }

    public class CreateVariacaoTamanhoDto
    {
        [Required(ErrorMessage = "Campo Tamanho é obrigatório")]
        public string Tamanho { get; set; }
        [Required(ErrorMessage = "Campo Estoque é obrigatório")]
        public int Estoque { get; set; }
       
        public string Cor { get; set; }
        public ICollection<CreateMedidasDto> Medidas { get; set; }
    }

    public class CreateMedidasDto
    {
        
        public string Regiao { get; set; }
        
        public double RegTam { get; set; }
    }

}
