using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestWithAspNetUdemy.Business;
using RestWithAspNetUdemy.Data.VO;
using RestWithAspNetUdemy.Model;

namespace RestWithAspNetUdemy.Controllers
{
    [ApiVersion("1")]
    [Authorize("Bearer")]
    [Route("api/[controller]/v{version:apiVersion}")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private ILoginBusiness _loginBusiness;

        public AuthController(ILoginBusiness loginBusiness)
        {
            _loginBusiness = loginBusiness;
        }

        [HttpPost]
        [Route("signin")]
        public IActionResult Signin([FromBody] UserVO userVO)
        {
            if (userVO == null) return BadRequest("Invalid Client Request");

            var token = _loginBusiness.ValidateCredential(userVO);
            if (token == null) return Unauthorized();
            return Ok(token);
        }

        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh([FromBody] TokenVO tokenVO)
        {
            if (tokenVO == null) return BadRequest("Invalid Client Request");

            var token = _loginBusiness.ValidateCredential(tokenVO);
            if (token == null) return BadRequest("Invalid Client Request");
            return Ok(token);
        }

        [HttpGet]
        [Route("revoke")]
        public IActionResult Revoke()
        {
            var username = User.Identity.Name;
            var result = _loginBusiness.RevokeToken(username);
            if (!result) return BadRequest("Invalid Client Request");
            return NoContent();
        }
    }
}
