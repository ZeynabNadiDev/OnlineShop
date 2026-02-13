using Asp.Versioning;
using IDP.Api.Controllers.BaseController;
using IDP.Application.Commands.Auth;
using IDP.Application.Commands.User;
using IDP.Application.Queries.Auth;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IDP.Api.Controllers.V1
{
    [ApiController]
    [ApiVersion(1)]
    [ApiVersion(2)]
    [Route("api/v{v:apiVersion}/Auth")]
    public class AuthController : IBaseController
    {
        public readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] AuthQuery authQuery)
        {
            var res = await _mediator.Send(authQuery);
            return Ok(res);
        }

        [HttpPost("RegisterAndSendOtp")]
        public async Task<IActionResult> RegisterAndSendOtp([FromBody] AuthCommand authCommand)
        {
            var res = await _mediator.Send(authCommand);
            return Ok(res);
        }
    }
}
