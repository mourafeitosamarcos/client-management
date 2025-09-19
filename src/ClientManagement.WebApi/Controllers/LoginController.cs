using ClientManagement.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using ClientManagement.Application.Dto;

namespace ClientManagement.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IClientService _clientService;

        public LoginController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO request)
        {
            var jwt = await _clientService.CheckLogin(request);
            if (string.IsNullOrEmpty(jwt))
            {
                return Unauthorized(new { message = "Invalid email or password." });
            }
            return Ok(new { token = jwt });
        }
    }
}
