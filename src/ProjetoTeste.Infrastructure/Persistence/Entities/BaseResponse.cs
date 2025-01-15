namespace ProjetoTeste.Arguments.Arguments.Response
{
    public class BaseResponse<TRequest>
    {
        public bool Success { get; set; }

        public string? Message { get; set; }
        public TRequest? Request { get; set; }
    }
}