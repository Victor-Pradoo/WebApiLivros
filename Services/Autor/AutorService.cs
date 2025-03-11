using Azure;
using Microsoft.EntityFrameworkCore;
using WebApi8_Livros.Data;
using WebApi8_Livros.Models;

namespace WebApi8_Livros.Services.Autor
{
    //AutorService recebe os métodos de AutorInterface
    public class AutorService : AutorInterface
    {
        private readonly AppDbContext _context;

        public AutorService(AppDbContext context) 
        {
            _context = context;
        }

        public async Task<ResponseModel<AutorModel>> BuscarAutorPorId(int idAutor)
        {
            ResponseModel<AutorModel> resposta = new ResponseModel<AutorModel>();
            try
            {
                var autor = await _context.Autores.FirstOrDefaultAsync(autorTable => autorTable.Id == idAutor); // Esse FirstOrDefaultAsync() retorna o primeiro registro que obedeça a uma regra específica, a regra é executada para cada linha da tabela, onde autorBanco representa cada linha

                if (autor == null)
                {
                    resposta.Mensagem = "Nenhum registro encontrado!";
                    return resposta;
                }

                resposta.Dados = autor;
                resposta.Mensagem = "Autor Localizado";

                return resposta;
            }

            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<AutorModel>> BuscarAutorPorIdLivro(int idLivro)
        {

            ResponseModel<AutorModel> resposta = new ResponseModel<AutorModel>();

            try
            {
                var autor = await _context.Livros.Include(livroTable => livroTable.Autor).FirstOrDefaultAsync(livroTable => livroTable.Id == idLivro); 
                // o Include() serve para entrarmos na propriedade Autor declarada como propriedade na classe Livro model, e retornarmos todas as informações do autor do livro daquela linha da tabela de livros. livroTable representa cada linha

                if (autor == null)
                {
                    resposta.Mensagem = "Nenhum registro encontrado!";
                    return resposta;
                }

                resposta.Dados = autor;
                resposta.Mensagem = "Autor Localizado";
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<AutorModel>>> ListarAutores()
        {
            ResponseModel<List<AutorModel>> resposta = new ResponseModel<List<AutorModel>>();
            try
            {   //o trecho de código usa o await para esperar a consulta à tabela Autores no banco e a transformação em lista serem finalizadas para então continuar a execução
                var autores = await _context.Autores.ToListAsync();

                resposta.Dados = autores;
                resposta.Mensagem = "Todos os autores foram coletados";

                return resposta;
            }
            catch (Exception ex) 
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }
    }
}
