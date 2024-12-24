namespace ProjetoTeste.Arguments.Arguments.Response
{
    public class Response<TRequest>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public TRequest? Request { get; set; }
    }
}
