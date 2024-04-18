using System.ComponentModel.DataAnnotations;
//dto eh um data objet tranfer e ele serve para abstarir o objeto do banco, no retornando diretamente o objeto do banco, e retornando apenas informcoes que voce quer, atraves
// de um map, que converte os objetos entre si
namespace FilmesAPI.Data.Dtos
{
    public class CreateFilmeDto
    {
        [Required(ErrorMessage = "O título é obrigatório")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O Gênero é obrigatório")]
        [StringLength(50, ErrorMessage = "Não pode exceder 50 caracteres")] //StringLenght faz o msm papel do MaxLenght mas nao aloca memoria dentro do banco
        public string Genero { get; set; }

        [Required(ErrorMessage = "A duração é obrigatório")]
        [Range(70, 600, ErrorMessage = "Duração deve ser entre 70 e 600 min")]
        public int Duracao { get; set; }
    }
}
