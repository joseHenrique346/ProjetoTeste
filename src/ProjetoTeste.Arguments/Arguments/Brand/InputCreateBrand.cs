using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments.Brand
{
    [method: JsonConstructor]
    public class InputCreateBrand(string name, string code, string description)
    {
        public string Name { get; } = name;
        public string Code { get; } = code;
        public string Description { get; } = description;
    }
}