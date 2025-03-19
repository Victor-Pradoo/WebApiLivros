using Microsoft.EntityFrameworkCore;
using WebApi8_Livros.Data;
using WebApi8_Livros.Dto.Autor;
using WebApi8_Livros.Dto.Livro;
using WebApi8_Livros.Models;

namespace WebApi8_Livros.Services.Livro
{
    public class LivroService : ILivroInterface
    {
        private readonly AppDbContext _context;

        public LivroService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<LivroModel>> BuscarLivroPorId(int idLivro)
        {
            var resposta = new ResponseModel<LivroModel>();

            try
            {
                var livro = await _context.Livros.Include(autorTabela => autorTabela.Autor).FirstOrDefaultAsync(livroTabela => livroTabela.Id == idLivro);

                if (livro == null)
                {
                    resposta.Mensagem = "Nenhum livro com este id foi encontrado";
                    return resposta;
                }

                resposta.Mensagem = "Livro encontrado com sucesso";
                resposta.Dados = livro;
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<LivroModel>>> BuscarLivrosPorIdAutor(int idAutor)
        {
            var resposta = new ResponseModel<List<LivroModel>>();

            try
            {
                var livro = await _context.Livros
                    .Include(autorTabela => autorTabela.Autor)
                    .Where(livrotabela => livrotabela.Autor.Id == idAutor) // método do AppDbContext diferente do usado no BuscarAutorPorIdLivro
                    .ToListAsync();

                if (livro == null)
                {
                    resposta.Mensagem = "Nenhum livro encontrado";
                    return resposta;
                }

                resposta.Mensagem = "Lista de livros retornada com sucesso";
                resposta.Dados = livro;
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<LivroModel>>> CriarLivro(LivroCriacaoDto livroCriacaoDto)
        {
            var resposta = new ResponseModel<List<LivroModel>>();

            try
            {
                var autor = await _context.Autores.FirstOrDefaultAsync(autorTabela => autorTabela.Id == livroCriacaoDto.Autor.Id); //esta validando Id do autor passado no LivroCriacaoDto

                if (autor == null)
                {
                    resposta.Mensagem = "O id do autor passado como parâmetro não se refere a nenhum autor na tabela de autores";
                    return resposta;
                }

                var novoLivro = new LivroModel()
                {
                    Titulo = livroCriacaoDto.Titulo,
                    Autor = autor
                };

                _context.Livros.Add(novoLivro);
                await _context.SaveChangesAsync();

                resposta.Mensagem = "Livro criado com sucesso";
                resposta.Dados = await _context.Livros.Include(a => a.Autor).ToListAsync();
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<LivroModel>>> EditarLivro(LivroEdicaoDto livroEdicaoDto)
        {
            ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();
            
            try
            {
                var livro = await _context.Livros.Include(livroTabela => livroTabela.Autor).FirstOrDefaultAsync(livrotabela => livrotabela.Id == livroEdicaoDto.Id);

                var autor = await _context.Autores.FirstOrDefaultAsync(autorTabela => autorTabela.Id == livroEdicaoDto.Autor.Id);

                if (autor == null)
                {
                    resposta.Mensagem = "Nenhum registro de autor localizado";
                    return resposta;
                }

                if (livro == null)
                {
                    resposta.Mensagem = "Nenhum registro de livro localizado";
                    return resposta;
                }

                livro.Titulo = livroEdicaoDto.Titulo;
                livro.Autor = autor;

                _context.Update(livro);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Livros.Include(a => a.Autor).ToListAsync();
                resposta.Mensagem = "Registro de livro editado com sucesso!";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<LivroModel>>> ExcluirLivro(int idLivro)
        {
            ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();

            try
            {
                var livroAExcluir = await _context.Livros.FirstOrDefaultAsync(livroTable => livroTable.Id == idLivro);

                if (livroAExcluir == null)
                {
                    resposta.Mensagem = "Nenhum livro com esse id localizado.";
                    return resposta;
                }

                _context.Livros.Remove(livroAExcluir);
                await _context.SaveChangesAsync();

                resposta.Dados = _context.Livros.Include(a => a.Autor).ToList();
                resposta.Mensagem = "Livro excluido com sucesso!";

                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<LivroModel>>> ListarLivros()
        {
            var resposta = new ResponseModel<List<LivroModel>>();

            try
            {
                var livros = await _context.Livros.Include(autorTabela => autorTabela.Autor).ToListAsync();

                if (livros == null)
                {
                    resposta.Mensagem = "Nenhum livro encontrado";
                    return resposta;
                }

                resposta.Mensagem = "Lista de livros retornada com sucesso";
                resposta.Dados = livros;
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
