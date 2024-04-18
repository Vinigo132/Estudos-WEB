using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmesController : ControllerBase
    {
        //injecao de dependencia, fazer o controlador usar o nosso context responsavel pela ligacao com o banco
        private FilmeContext _context;
        private IMapper _mapper;
        public FilmesController(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //doc: 
        /// <summary>
        /// Adiciona um filme ao banco de dados
        /// </summary>
        /// <param name="filmeDto">Objeto com os campos necessários para criação de um filme</param>
        /// <returns>IActionResult</returns>
        /// <response code="201">Caso inserção seja feita com sucesso</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)] //qual o tipo de retorno efetivo do método
        //from Body quer dizer q vem do corpo da requisiçao
        //padrao do rest é retornar o objeto criado(201) e o caminho dele
        //nao eh uma boa pratica deixar o modelo de banco exposto no Controller, entao eh bom usar um Dto, fazendo as validacoes de maneira mas correta.
        public IActionResult AdicionarFilme([FromBody] CreateFilmeDto filmeDto)
        {
            Filme filme = _mapper.Map<Filme>(filmeDto);
            _context.Filmes.Add(filme);
            _context.SaveChanges();
            Console.WriteLine("Filme adicionado");
            return CreatedAtAction(nameof(RecuperarFilmePorId), new { id = filme.Id}, filme);
        }

        [HttpGet]
        //fromQuery explicita que o usario vai passar isso atraves de uma consulta / query: filmes?skip=x&take=y // skip = x nos parametros seria o valor padrao
        public IEnumerable<ReadFilmeDto> RecuperaFilme([FromQuery] int skip = 0, [FromQuery] int take = 50)
        {
            return _mapper.Map<List<ReadFilmeDto>>(_context.Filmes.Skip(skip).Take(take));
        }

        [HttpGet("{id}")]
        //filme pode retornar nulo '?'
        //interface de resultado de uma acao q foi executada, usa-se isso pois notFound404 é o retorno ideal para esse caso, nao o 204(concluido mas sem retorno).
        public IActionResult RecuperarFilmePorId(int id) 
        {
            //first ou default pois se nao achar algum filme, ele retorna nulo, no caso.
            var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme == null) return NotFound();
            var filmeDto = _mapper.Map<ReadFilmeDto>(filme);
            return Ok(filmeDto);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaFilme(int id, [FromBody] UpdateFilmeDto filmeDto) 
        {
            var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme == null) return NotFound();
            _mapper.Map(filmeDto, filme);
            _context.SaveChanges();
            return NoContent();
        }

        //atualizacao parcial, sem precisar passar todas as informacoes.
        [HttpPatch("{id}")]
        public IActionResult AtualizaFilmeParcial(int id, JsonPatchDocument<UpdateFilmeDto> patch)
        {
            var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme == null) return NotFound();

            //para ser possivel de verificar se a informacao eh valida, pois n da pra aplicar as data anotations automaticamente com JsonPatch
            //converte filme pra dto, pra verificar, se tiver ok, converte pra filme dnv
            var filmePraAtt = _mapper.Map<UpdateFilmeDto>(filme);
            patch.ApplyTo(filmePraAtt, ModelState);

            if (!TryValidateModel(filmePraAtt))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(filmePraAtt, filme);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletaFilme(int id) 
        {
            var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme == null) return NotFound();
            _context.Filmes.Remove(filme);
            _context.SaveChanges();
            return NoContent();
        }

    }
}
