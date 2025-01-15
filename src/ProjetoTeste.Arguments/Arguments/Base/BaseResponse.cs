using ProjetoTeste.Arguments.Arguments.Base;

namespace ProjetoTeste.Arguments.Arguments.Response
{
    public class BaseResponse<TContent>
    {
        public bool Success { get; set; } = true;
        public List<Notification>? Message { get; set; } = new List<Notification>();
        public TContent? Content { get; set; }

        public bool AddSuccessMessage(string message)
        {
            Message.Add(new Notification(message, EnumNotificationType.Success));
            return true;
        }
        public bool AddErrorMessage(string message)
        {
            Message.Add(new Notification(message, EnumNotificationType.Error));
            return true;
        }
    }
}