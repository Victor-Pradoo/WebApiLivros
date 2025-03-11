using Microsoft.EntityFrameworkCore;
using WebApi8_Livros.Models;
namespace WebApi8_Livros.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {

        }

        public DbSet<AutorModel> Autores { get; set; }
        public DbSet<LivroModel> Livros { get; set; }

    }
}
