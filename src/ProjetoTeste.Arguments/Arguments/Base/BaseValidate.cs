namespace ProjetoTeste.Arguments.Arguments.Base
{
    public class BaseValidate
    {
        public bool Invalid { get; set; }

        public bool SetInvalid()
        {
            return Invalid = true;
        }
    }
}
