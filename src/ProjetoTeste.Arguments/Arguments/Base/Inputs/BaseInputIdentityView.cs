using ProjetoTeste.Arguments.Arguments.Base.Inputs.Interfaces;
using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments.Base.Inputs
{
    public class BaseInputIdentityView<TInputIdentityView> where TInputIdentityView : BaseInputIdentityView<TInputIdentityView>, IBaseIdentityView { }

    [method: JsonConstructor]
    public class BaseIdentityView_0(long id) : BaseInputIdentityView<BaseIdentityView_0>, IBaseIdentityView
    {
        public long Id { get; } = id;
    }
}