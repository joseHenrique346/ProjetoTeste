namespace ProjetoTeste.Arguments.Arguments.Brand
{
    [method: JsonConstructor]
    public class OutputBrand
    {
        public long Id { get; }
        public string Name { get; }
        public string Code { get; }
        public string Description { get; }

        public OutputBrand(long id, string name, string code, string description)
        {
            Id = id;
            Name = name;
            Code = code;
            Description = description;
        }
    }
}