using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Models
{
    public class Filme
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "O título é obrigatório")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O Gênero é obrigatório")]
        [MaxLength(50, ErrorMessage = "Não pode exceder 50 caracteres")]
        public string Genero { get; set; }

        [Required(ErrorMessage = "A duração é obrigatório")]
        [Range(70, 600, ErrorMessage = "Duração deve ser entre 70 e 600 min")]
        public int Duracao { get; set; }
    }
}
