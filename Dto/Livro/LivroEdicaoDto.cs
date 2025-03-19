using WebApi8_Livros.Dto.Vinculos;
using WebApi8_Livros.Models;

namespace WebApi8_Livros.Dto.Livro
{
    public class LivroEdicaoDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public LivroAutorVinculo Autor { get; set; }
    }
}
