using ProjetoTeste.Arguments.Arguments.Product;

namespace ProjetoTeste.Arguments.Arguments;

public class OutputBrandWithProducts
{
    public long Id { get; }
    public string Name { get; }
    public string Code { get; }
    public string Description { get; }
    public List<OutputProduct>? ListProduct { get; set; }

    public OutputBrandWithProducts(long id, string name, string code, string description, List<OutputProduct>? listProduct)
    {
        Id = id;
        Name = name;
        Code = code;
        Description = description;
        ListProduct = listProduct;
    }
}