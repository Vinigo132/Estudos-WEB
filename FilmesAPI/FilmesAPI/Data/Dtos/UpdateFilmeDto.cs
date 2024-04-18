using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.Dtos
{
    public class UpdateFilmeDto
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
