using ProjetoTeste.Arguments.Arguments.Product;
using ProjetoTeste.Arguments.Arguments.Response;
using ProjetoTeste.Infrastructure.Interface.Service;

namespace ProjetoTeste.Infrastructure.Service
{
    public class ProductValidateService : IProductValidateService
    {
        #region Validate Create

        public async Task<BaseResponse<List<ProductValidate?>>> ValidateCreateProduct(List<ProductValidate> listInputCreateProduct)
        {
            var response = new BaseResponse<List<ProductValidate?>>();

            _ = (from i in listInputCreateProduct
                 where i.ExistingCodeProduct != null
                 let setInvalid = i.SetInvalid()
                 let message = response.AddErrorMessage($"Erro ao criar o produto {i.InputCreateProduct.Name}, o código {i.InputCreateProduct.Code} já está sendo utilizado")
                 select i).ToList();

            _ = (from i in listInputCreateProduct
                 where i.RepeatedCode != null
                 let setInvalid = i.SetInvalid()
                 let message = response.AddErrorMessage($"Erro ao criar o produto {i.InputCreateProduct.Name}, o código {i.InputCreateProduct.Code} está sendo utilizado mais de uma vez na requisição")
                 select i).ToList();

            _ = (from i in listInputCreateProduct
                 where i.InputCreateProduct.Name.Length > 40 || string.IsNullOrEmpty(i.InputCreateProduct.Name) || string.IsNullOrWhiteSpace(i.InputCreateProduct.Name)
                 let setInvalid = i.SetInvalid()
                 let message = response.AddErrorMessage(i.InputCreateProduct.Name.Length > 40 ? $"Erro ao criar o produto {i.InputCreateProduct.Name}, o nome ultrapassou o limite de 40 caracteres" : $"Erro ao criar o produto {i.InputCreateProduct.Name}, o nome não possue valor")
                 select i).ToList();

            _ = (from i in listInputCreateProduct
                 where i.InputCreateProduct.Code.Length > 6 || string.IsNullOrEmpty(i.InputCreateProduct.Code) || string.IsNullOrWhiteSpace(i.InputCreateProduct.Code)
                 let setInvalid = i.SetInvalid()
                 let message = response.AddErrorMessage(i.InputCreateProduct.Code.Length > 6 ? $"Erro ao criar o produto {i.InputCreateProduct.Code},  o código ultrapassou o limite de 6 caracteres." : $"Erro ao criar o produto {i.InputCreateProduct.Code}, o código não possue valor.")
                 select i).ToList();

            _ = (from i in listInputCreateProduct
                 where i.InputCreateProduct.Description.Length > 100 || string.IsNullOrEmpty(i.InputCreateProduct.Description) || string.IsNullOrWhiteSpace(i.InputCreateProduct.Description)
                 let setInvalid = i.SetInvalid()
                 let message = response.AddErrorMessage(i.InputCreateProduct.Description.Length > 100 ? $"Erro ao criar o produto {i.InputCreateProduct.Code}, a descrição ultrapassou o limite de 100 caracteres." : $"Erro ao criar o produto {i.InputCreateProduct.Code}, a descrição não possue valor.")
                 select i).ToList();

            _ = (from i in listInputCreateProduct
                 where i.InputCreateProduct.Stock.ToString().Length > 10 || string.IsNullOrEmpty(i.InputCreateProduct.Stock.ToString()) || string.IsNullOrWhiteSpace(i.InputCreateProduct.Stock.ToString())
                 let setInvalid = i.SetInvalid()
                 let message = response.AddErrorMessage(i.InputCreateProduct.Stock.ToString().Length > 10 ? $"Erro ao criar o produto {i.InputCreateProduct.Name}, o tamanho do estoque é inválido" : $"Erro ao criar o produto {i.InputCreateProduct.Name}, o estoque não possue valor")
                 select i).ToList();

            _ = (from i in listInputCreateProduct
                 where i.InputCreateProduct.Stock < 0
                 let setInvalid = i.SetInvalid()
                 let message = response.AddErrorMessage($"Erro ao criar o produto {i.InputCreateProduct.Name}, o estoque não pode ser menor que zero")
                 select i).ToList();

            _ = (from i in listInputCreateProduct
                 where i.InputCreateProduct.Price.ToString().Length > 10 || string.IsNullOrEmpty(i.InputCreateProduct.Price.ToString()) || string.IsNullOrWhiteSpace(i.InputCreateProduct.Price.ToString())
                 let setInvalid = i.SetInvalid()
                 let message = response.AddErrorMessage(i.InputCreateProduct.Price.ToString().Length > 10 ? $"Erro ao criar o produto {i.InputCreateProduct.Name}, o preço é inválido" : $"Erro ao criar o produto {i.InputCreateProduct.Name}, o preço não possue valor")
                 select i).ToList();

            _ = (from i in listInputCreateProduct
                 where i.InputCreateProduct.Price < 0
                 let setInvalid = i.SetInvalid()
                 let message = response.AddErrorMessage($"Erro ao criar o produto {i.InputCreateProduct.Name}, o preço não pode ser menor que zero")
                 select i).ToList();

            _ = (from i in listInputCreateProduct
                 where i.ExistingBrand == 0
                 let setInvalid = i.SetInvalid()
                 let message = response.AddErrorMessage($"Erro ao criar o produto {i.InputCreateProduct.Name}, o Id da marca é inválido")
                 select i).ToList();

            var selectedValidListCreate = (from i in listInputCreateProduct
                                           where !i.Invalid
                                           select i).ToList();

            if (!selectedValidListCreate.Any())
            {
                response.Success = false;
                return response;
            }

            response.Content = selectedValidListCreate;
            return response;
        }

        #endregion

        #region Validate Update

        public async Task<BaseResponse<List<ProductValidate?>>> ValidateUpdateProduct(List<ProductValidate> listProductValidate)
        {
            var response = new BaseResponse<List<ProductValidate?>>();

            var listValidateIds = (from i in listProductValidate
                                   where i.CurrentProduct != i.InputIdentityUpdateProduct.Id
                                   select i).ToList();

            _ = (from i in listValidateIds
                 where i == null
                 let setInvalid = i.SetInvalid()
                 let message = response.AddErrorMessage($"Erro ao atualizar o produto {i.InputIdentityUpdateProduct.InputUpdateProduct.Name}, seu Id: {i.InputIdentityUpdateProduct.Id} está inválido.")
                 select i).ToList();

            _ = (from i in listProductValidate
                 where i.ExistingCodeProduct != null && i.InputIdentityUpdateProduct.Id != i.CurrentProduct
                 let setInvalid = i.SetInvalid()
                 let message = response.AddErrorMessage($"Erro ao atualizar o produto {i.InputIdentityUpdateProduct.InputUpdateProduct.Name}, seu código: {i.InputIdentityUpdateProduct.InputUpdateProduct.Code} já sendo utilizado")
                 select i).ToList();

            _ = (from i in listProductValidate
                 where i.InputIdentityUpdateProduct.InputUpdateProduct.Name.Length > 40
                 || string.IsNullOrEmpty(i.InputIdentityUpdateProduct.InputUpdateProduct.Name)
                 || string.IsNullOrWhiteSpace(i.InputIdentityUpdateProduct.InputUpdateProduct.Name)
                 let setInvalid = i.SetInvalid()
                 let message = response.AddErrorMessage(i.InputIdentityUpdateProduct.InputUpdateProduct.Name.Length > 40 ? $"Erro ao atualizar o produto {i.InputIdentityUpdateProduct.InputUpdateProduct.Name}, o nome ultrapassa o limite de 40 caracteres" : $"Erro ao atualizar o produto {i.InputIdentityUpdateProduct.InputUpdateProduct.Name}, o nome não foi preenchido corretamente")
                 select i).ToList();

            _ = (from i in listProductValidate
                 where i.InputIdentityUpdateProduct.InputUpdateProduct.Code.Length > 6
                 || string.IsNullOrEmpty(i.InputIdentityUpdateProduct.InputUpdateProduct.Code)
                 || string.IsNullOrWhiteSpace(i.InputIdentityUpdateProduct.InputUpdateProduct.Code)
                 let setInvalid = i.SetInvalid()
                 let message = response.AddErrorMessage(i.InputIdentityUpdateProduct.InputUpdateProduct.Code.Length > 6 ? $"Erro ao atualizar o produto {i.InputIdentityUpdateProduct.InputUpdateProduct.Name}, o código ultrapassa o limite de 6 caracteres" : $"Erro ao atualizar o produto {i.InputIdentityUpdateProduct.InputUpdateProduct.Name}, o código não foi preenchido corretamente")
                 select i).ToList();

            _ = (from i in listProductValidate
                 where i.InputIdentityUpdateProduct.InputUpdateProduct.Description.Length > 100
                 || string.IsNullOrEmpty(i.InputIdentityUpdateProduct.InputUpdateProduct.Description)
                 || string.IsNullOrWhiteSpace(i.InputIdentityUpdateProduct.InputUpdateProduct.Description)
                 let setInvalid = i.SetInvalid()
                 let message = response.AddErrorMessage(i.InputIdentityUpdateProduct.InputUpdateProduct.Code.Length > 6 ? $"Erro ao atualizar o produto {i.InputIdentityUpdateProduct.InputUpdateProduct.Name}, a descrição ultrapassa o limite de 100 caracteres" : $"Erro ao atualizar o produto {i.InputIdentityUpdateProduct.InputUpdateProduct.Name}, a descrição não foi preenchido corretamente")
                 select i).ToList();

            _ = (from i in listProductValidate
                 where i.InputIdentityUpdateProduct.InputUpdateProduct.Stock < 0
                 || i.InputIdentityUpdateProduct.InputUpdateProduct.Stock.ToString().Length > 10
                 let setInvalid = i.SetInvalid()
                 let message = response.AddErrorMessage(i.InputIdentityUpdateProduct.InputUpdateProduct.Stock < 0 ? $"Erro ao atualizar o produto {i.InputIdentityUpdateProduct.InputUpdateProduct.Name}, o estoque não pode ser menor que zero." : $"Erro ao atualizar o produto {i.InputIdentityUpdateProduct.InputUpdateProduct.Name}, o estoque não pode ter este tamanho.")
                 select i).ToList();

            _ = (from i in listProductValidate
                 where i.InputIdentityUpdateProduct.InputUpdateProduct.Price < 0
                 let setInvalid = i.SetInvalid()
                 let message = response.AddErrorMessage($"Erro ao atualizar o produto {i.InputIdentityUpdateProduct.InputUpdateProduct.Name}, o preço não pode ser menor que zero.")
                 select i).ToList();

            _ = (from i in listProductValidate
                 where i.ExistingBrand == 0
                 let setInvalid = i.SetInvalid()
                 let message = response.AddErrorMessage($"Erro ao atualizar o produto {i.InputIdentityUpdateProduct.InputUpdateProduct.Name}, o Id da marca está inválido.")
                 select i).ToList();

            var selectedValidProducts = (from i in listProductValidate
                                         where !i.Invalid
                                         select i).ToList();

            if (!selectedValidProducts.Any())
            {
                response.Success = false;
                return response;
            }

            response.Content = selectedValidProducts;
            return response;
        }

        #endregion

        #region Validate Delete

        public async Task<BaseResponse<List<ProductValidate?>>> ValidateDeleteProduct(List<ProductValidate> listProductValidate)
        {
            var response = new BaseResponse<List<ProductValidate?>>();

            _ = (from i in listProductValidate
                 where i.InputIdentityDeleteProduct.Id == 0
                 let setInvalid = i.SetInvalid()
                 let message = response.AddErrorMessage($"Não foi possível deletar o produto do Id: {i.InputIdentityDeleteProduct.Id}, Id inválido.")
                 select i).ToList();

            _ = (from i in listProductValidate
                 where i.Product.Stock > 0
                 let setInvalid = i.SetInvalid()
                 let message = response.AddErrorMessage($"Não foi possível deletar o produto do Id: {i.InputIdentityDeleteProduct.Id}, o produto {i.Product.Name} ainda possue estoque")
                 select i).ToList();

            var selectedValidDelete = (from i in listProductValidate
                                       where !i.Invalid
                                       select i).ToList();

            if (!selectedValidDelete.Any())
            {
                response.Success = false;
                return response;
            }

            response.Content = selectedValidDelete;
            return response;
        }
    }
}
#endregion