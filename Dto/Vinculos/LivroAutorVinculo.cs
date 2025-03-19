namespace WebApi8_Livros.Dto.Vinculos
{
    public class LivroAutorVinculo
    {
        // No momento de criar um novo livro o compilador comenta da falta do preenchimento da propriedade livros presente em AutorModel, por conta disso foi criada esta classe para ser utilizada como o tipo da propriedade autor em LivroEdicaoDto

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
    }
}
