using FilmesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Data
{
    public class FilmeContext : DbContext
    {
        //configuracoes / opcoes de acesso no parametro, e passando para o construtor base(DbContext)
        public FilmeContext(DbContextOptions<FilmeContext> opts) : base(opts)
        { 
           
        }

        public DbSet<Filme> Filmes { get; set; }
    }
}
