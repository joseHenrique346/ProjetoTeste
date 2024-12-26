using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProjetoTeste.Api.Controllers
{
    [Route("client")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ClientController
    {
    }
}