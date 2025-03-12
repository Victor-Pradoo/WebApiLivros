using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi8_Livros.Dto.Autor;
using WebApi8_Livros.Models;
using WebApi8_Livros.Services.Autor;

namespace WebApi8_Livros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        private readonly AutorInterface _autorInterface;
        public AutorController(AutorInterface autorInterface) 
        {
            _autorInterface = autorInterface;
        }

        [HttpGet("ListarAutores")]
        public async Task<ActionResult<ResponseModel<List<AutorModel>>>> ListarAutores()
        {
            var autores = await _autorInterface.ListarAutores();
            return Ok(autores);
        }

        [HttpGet("BuscarAutorPorId/{idAutor}")]
        public async Task<ActionResult<ResponseModel<AutorModel>>> BuscarAutorPorId(int idAutor)
        {
            var autor = await _autorInterface.BuscarAutorPorId(idAutor);
            return Ok(autor);
        }

        [HttpGet("BuscarAutorPorIdLivro/{idLivro}")]
        public async Task<ActionResult<ResponseModel<AutorModel>>> BuscarAutorPorIdLivro(int idLivro)
        {
            var autor = await _autorInterface.BuscarAutorPorId(idLivro);
            return Ok(autor);
        }

        [HttpPost("CriarAutor")]
        public async Task<ActionResult<ResponseModel<List<AutorModel>>>> CriarAutor (AutorCriacaoDto autorCriacaoDto)
        {
            var novoAutor = await _autorInterface.CriarAutor(autorCriacaoDto);
            return Ok(novoAutor);
        }

        [HttpPut("EditarAutor")]
        public async Task<ActionResult<ResponseModel<List<AutorModel>>>> EditarAutor(AutorEdicaoDto autorEdicaoDto)
        {
            var autorEditado = await _autorInterface.EditarAutor(autorEdicaoDto);
            return Ok(autorEditado);
        }

        [HttpDelete("ExcluirAutorPorId")]
        public async Task<ActionResult<ResponseModel<List<AutorModel>>>> ExcluirAutorPorId(int idAutor)
        {
            var autorExcluido = await _autorInterface.ExcluirAutor(idAutor);
            return Ok(autorExcluido);
        }


    }
}
