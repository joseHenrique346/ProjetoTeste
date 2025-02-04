using ProjetoTeste.Arguments.Arguments.Base.Inputs;
using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments.Brand
{
    [method: JsonConstructor]
    public class InputIdentityUpdateBrand(long id, InputUpdateBrand inputUpdateBrand) : BaseInputIdentityUpdate<InputIdentityUpdateBrand>
    {
        public long Id { get; private set; } = id;
        public InputUpdateBrand InputUpdateBrand { get; private set; } = inputUpdateBrand;
    }
}
