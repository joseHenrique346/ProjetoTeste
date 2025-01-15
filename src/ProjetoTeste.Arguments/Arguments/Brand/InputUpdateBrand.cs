using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments.Brand
{
    [method: JsonConstructor]
    public class InputUpdateBrand(long id, string name, string code, string description)
    {
        public long Id { get; } = id;
        public string Name { get; } = name;
        public string Code { get; } = code;
        public string Description { get; } = description;
    }
}