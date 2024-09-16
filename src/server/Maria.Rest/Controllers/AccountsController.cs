using Maria.Application.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Maria.Rest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : Controller
    {
        private readonly IAccountsService _accountsService;

        public AccountsController(IAccountsService accountsService)
        {
            _accountsService = accountsService;
        }

        [HttpPost("CreateAccount")]
        public async ValueTask<IActionResult> CreateAccount(CreateUserRequest request)
        {
            var id = await _accountsService.CreateUser(request);
            return Created();
        }

        [HttpPost("Login")]
        public async ValueTask<IActionResult> Login(LoginRequest request)
        {
            var response = await _accountsService.Login(request);
            if (response == null)
                return BadRequest("Usuário ou senha inválido.");

            return Ok(response);

        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAuth()
        {
            var tokenValue = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            
            var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var userName = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;

            return Ok(new { id = int.Parse(userId), nome = userName, token = tokenValue });
        }
    }
}
