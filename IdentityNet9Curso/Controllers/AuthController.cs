using IdentityNet9Curso.Dtos;
using IdentityNet9Curso.Models;
using IdentityNet9Curso.Services.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityNet9Curso.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthInterface _authInterface;

        public AuthController(IAuthInterface authInterface)
        {
            _authInterface = authInterface;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ResponseModel<string>>> Register([FromBody] RegistroDto registroDto)
        {
            var resposta = await _authInterface.Register(registroDto);

            if (!resposta.Status)
            {
                return BadRequest(resposta);
            }

            return Ok(resposta);
        }
    }
}