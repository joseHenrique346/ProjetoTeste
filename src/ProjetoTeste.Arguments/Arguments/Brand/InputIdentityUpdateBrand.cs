using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments.Brand
{
    [method: JsonConstructor]
    public class InputIdentityUpdateBrand(long id, InputUpdateBrand inputUpdateBrand)
    {
        public long Id { get; private set; } = id;
        public InputUpdateBrand InputUpdateBrand { get; private set; } = inputUpdateBrand;
    }
}
