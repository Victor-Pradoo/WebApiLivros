using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using WebApi8_Livros.Dto.Vinculos;

namespace WebApi8_Livros.Dto.Livro
{
    public class LivroCriacaoDto
    {
        public string Titulo {  get; set; }
        public LivroAutorVinculo Autor { get; set; }   

    }
}
