using ApiAddressTestAspNet.DTO;
using ApiAddressTestAspNet.Models;
using ApiAddressTestAspNet.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiAddressTestAspNet.Controllers
{
    [Route("api/", Name = "Auth")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private IJwtService _jwtService;
        ILogger<TokenController> _logger;

        public TokenController(IJwtService jwtService, ILogger<TokenController> logger)
        {
            _jwtService = jwtService;
            _logger = logger;
        }


        // POST: api/auth
        /// <summary>
        /// Gerar Token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <response code="200">Retorna um token</response>
        /// <response code="401">Indica falha na autenticação</response>
        [ProducesResponseType(typeof(ReturnTokenDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ReturnBaseDTO), StatusCodes.Status401Unauthorized)]
        [AllowAnonymous]
        [HttpPost("auth")]
        public async Task<IActionResult> Authenticate([FromBody] User user)
        {
            ReturnDTO returnDTO = await _jwtService.Authenticate(user.Username, user.Password);

            if (returnDTO.Success)
            {
                return new OkObjectResult(returnDTO);
            }

            return new UnauthorizedObjectResult(returnDTO);
        }
    }
}
