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
                 where i.ExistingCodeId != null
                 let name = i.InputCreateProduct.Name
                 let code = i.InputCreateProduct.Code
                 let setInvalid = i.SetInvalid()
                 let message = response.AddErrorMessage($"Erro ao criar o produto {name}, o código {code} já está sendo utilizado")
                 select i).ToList();

            _ = (from i in listInputCreateProduct
                 where i.RepeatedCode != null
                 let name = i.InputCreateProduct.Name
                 let code = i.InputCreateProduct.Code
                 let setInvalid = i.SetInvalid()
                 let message = response.AddErrorMessage($"Erro ao criar o produto {name}, o código {code} está sendo utilizado mais de uma vez na requisição")
                 select i).ToList();

            _ = (from i in listInputCreateProduct.Select((value, index) => new { Value = value, Index = index })
                 let name = i.Value.InputCreateProduct.Name
                 where string.IsNullOrWhiteSpace(name) || name?.Length > 40
                 let index = i.Index + 1
                 let setInvalid = i.Value.SetInvalid()
                 let message = response.AddErrorMessage(string.IsNullOrWhiteSpace(name)
                    ? $"Erro ao criar o produto na {index}° posição, o nome não foi preenchido corretamente."
                    : $"Erro ao criar o produto {name}, o nome ultrapassou o limite de 40 caracteres")
                 select i).ToList();

            _ = (from i in listInputCreateProduct
                 let code = i.InputCreateProduct.Code
                 where string.IsNullOrWhiteSpace(code) || code?.Length > 6
                 let setInvalid = i.SetInvalid()
                 let message = response.AddErrorMessage(string.IsNullOrWhiteSpace(code)
                    ? $"Erro ao criar o produto {code}, o código não foi preenchido corretamente."
                    : $"Erro ao criar o produto {code}, o código ultrapassou o limite de 6 caracteres")
                 select i).ToList();

            _ = (from i in listInputCreateProduct
                 let description = i.InputCreateProduct.Description
                 where string.IsNullOrWhiteSpace(description) || description?.Length > 100
                 let code = i.InputCreateProduct.Code
                 let setInvalid = i.SetInvalid()
                 let message = response.AddErrorMessage(string.IsNullOrWhiteSpace(description)
                    ? $"Erro ao criar o produto {code}, a descrição não foi preenchida corretamente."
                    : $"Erro ao criar o produto {code}, a descrição ultrapassou o limite de 100 caracteres")
                 select i).ToList();

            _ = (from i in listInputCreateProduct
                 let stock = i.InputCreateProduct.Stock
                 where string.IsNullOrWhiteSpace(stock.ToString()) || stock.ToString().Length > 10
                 let name = i.InputCreateProduct.Name
                 let setInvalid = i.SetInvalid()
                 let message = response.AddErrorMessage(string.IsNullOrWhiteSpace(stock.ToString())
                    ? $"Erro ao criar o produto {name}, o estoque não foi preenchido corretamente."
                    : $"Erro ao criar o produto {name}, o tamanho do estoque é inválido")
                 select i).ToList();

            _ = (from i in listInputCreateProduct
                 where i.InputCreateProduct.Stock < 0
                 let name = i.InputCreateProduct.Name
                 let setInvalid = i.SetInvalid()
                 let message = response.AddErrorMessage($"Erro ao criar o produto {name}, o estoque não pode ser menor que zero")
                 select i).ToList();

            _ = (from i in listInputCreateProduct
                 let price = i.InputCreateProduct.Price
                 where string.IsNullOrWhiteSpace(price.ToString()) || price.ToString().Length > 10
                 let name = i.InputCreateProduct.Name
                 let setInvalid = i.SetInvalid()
                 let message = response.AddErrorMessage(string.IsNullOrWhiteSpace(price.ToString())
                    ? $"Erro ao criar o produto {name}, o preço não foi preenchido corretamente"
                    : $"Erro ao criar o produto {name}, o preço não pode ter este tamanho")
                 select i).ToList();

            _ = (from i in listInputCreateProduct
                 where i.InputCreateProduct.Price < 0
                 let name = i.InputCreateProduct.Name
                 let setInvalid = i.SetInvalid()
                 let message = response.AddErrorMessage($"Erro ao criar o produto {name}, o preço não pode ser menor que zero")
                 select i).ToList();

            _ = (from i in listInputCreateProduct
                 where i.ExistingBrand == 0
                 let name = i.InputCreateProduct.Name
                 let setInvalid = i.SetInvalid()
                 let message = response.AddErrorMessage($"Erro ao criar o produto {name}, o Id da marca é inválido")
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

            _ = (from i in listProductValidate
                 let id = i.InputIdentityUpdateProduct.Id
                 where id != i.CurrentProduct
                 let name = i.InputIdentityUpdateProduct.InputUpdateProduct.Name
                 let setInvalid = i.SetInvalid()
                 let message = response.AddErrorMessage($"Erro ao atualizar o produto {name}, seu Id: {id} está inválido.")
                 select i).ToList();

            _ = (from i in listProductValidate
                 where i.ExistingCodeProduct != null && i.InputIdentityUpdateProduct.Id != i.ExistingCodeProduct?.Id
                 let name = i.InputIdentityUpdateProduct.InputUpdateProduct.Name
                 let code = i.InputIdentityUpdateProduct.InputUpdateProduct.Code
                 let setInvalid = i.SetInvalid()
                 let message = response.AddErrorMessage($"Erro ao atualizar o produto {name}, seu código: {code} já sendo utilizado")
                 select i).ToList();

            _ = (from i in listProductValidate.Select((value, index) => new { Value = value, Index = index })
                 let name = i.Value.InputIdentityUpdateProduct.InputUpdateProduct.Name
                 where string.IsNullOrWhiteSpace(name) || name?.Length > 40
                 let index = i.Index + 1
                 let setInvalid = i.Value.SetInvalid()
                 let errorMessage = response.AddErrorMessage(string.IsNullOrWhiteSpace(name)
                    ? $"Erro ao atualizar o produto na {index}° posição, o nome não foi preenchido corretamente."
                    : $"Erro ao atualizar o produto {name}, o nome ultrapassa o limite de 40 caracteres")
                 select i).ToList();

            _ = (from i in listProductValidate
                 let code = i.InputIdentityUpdateProduct.InputUpdateProduct.Code
                 where string.IsNullOrWhiteSpace(code) || code?.Length > 6
                 let name = i.InputIdentityUpdateProduct.InputUpdateProduct.Name
                 let setInvalid = i.SetInvalid()
                 let message = response.AddErrorMessage(string.IsNullOrWhiteSpace(code)
                    ? $"Erro ao atualizar o produto {name}, o código não foi preenchido corretamente."
                    : $"Erro ao atualizar o produto {name}, o código ultrapassa o limite de 6 caracteres")
                 select i).ToList();

            _ = (from i in listProductValidate
                 let description = i.InputIdentityUpdateProduct.InputUpdateProduct.Description
                 where string.IsNullOrWhiteSpace(description) || description?.Length > 100
                 let name = i.InputIdentityUpdateProduct.InputUpdateProduct.Name
                 let setInvalid = i.SetInvalid()
                 let message = response.AddErrorMessage(string.IsNullOrWhiteSpace(description)
                    ? $"Erro ao atualizar o produto {name}, a descrição não foi preenchida corretamente"
                    : $"Erro ao atualizar o produto {name}, a descrição ultrapassa o limite de 100 caracteres")
                 select i).ToList();

            _ = (from i in listProductValidate
                 let stock = i.InputIdentityUpdateProduct.InputUpdateProduct.Stock
                 where stock < 0 || stock.ToString().Length > 10
                 let name = i.InputIdentityUpdateProduct.InputUpdateProduct.Name
                 let setInvalid = i.SetInvalid()
                 let errorMessage = response.AddErrorMessage(stock < 0
                    ? $"Erro ao atualizar o produto {name}, o estoque não pode ser menor que zero."
                    : $"Erro ao atualizar o produto {name}, o estoque não pode ter este tamanho.")
                 select i).ToList();

            _ = (from i in listProductValidate
                 where i.InputIdentityUpdateProduct.InputUpdateProduct.Price < 0
                 let name = i.InputIdentityUpdateProduct.InputUpdateProduct.Name
                 let setInvalid = i.SetInvalid()
                 let message = response.AddErrorMessage($"Erro ao atualizar o produto {name}, o preço não pode ser menor que zero.")
                 select i).ToList();

            _ = (from i in listProductValidate
                 where i.ExistingBrand == 0
                 let name = i.InputIdentityUpdateProduct.InputUpdateProduct.Name
                 let setInvalid = i.SetInvalid()
                 let message = response.AddErrorMessage($"Erro ao atualizar o produto {name}, o Id da marca está inválido.")
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
                 let id = i.InputIdentityDeleteProduct.Id
                 where i.RepeatedId != 0
                 let setInvalid = i.SetInvalid()
                 let message = response.AddErrorMessage($"Não foi possível deletar o produto do Id: {id}, Id foi digitado mais de uma vez na requisição")
                 select i).ToList();

            _ = (from i in listProductValidate
                 let id = i.InputIdentityDeleteProduct.Id
                 where i.Product == null
                 let setInvalid = i.SetInvalid()
                 let message = response.AddErrorMessage($"Não foi possível deletar o produto do Id: {id}, Id inválido.")
                 select i).ToList();

            _ = (from i in listProductValidate
                 let id = i.InputIdentityDeleteProduct.Id
                 where i.Product != null && i.Product.Stock > 0
                 let name = i.Product.Name
                 let setInvalid = i.SetInvalid()
                 let message = response.AddErrorMessage($"Não foi possível deletar o produto do Id: {id}, o produto {name} ainda possue estoque")
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
        #endregion
    }
}