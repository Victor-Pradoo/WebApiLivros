namespace WebApi8_Livros.Models
{
    // <T> = tipo qualquer
    // ? = pode ser nulo
    public class ResponseModel<T>
    {
        public T? Dados {get; set; }
        public string Mensagem { get; set; } = string.Empty; //valor padrão caso n seja preenchido
        public bool Status { get; set; } = true;
    }
}
