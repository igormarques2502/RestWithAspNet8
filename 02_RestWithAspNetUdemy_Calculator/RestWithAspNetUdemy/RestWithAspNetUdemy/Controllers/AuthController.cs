using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using RestWithAspNetUdemy.Business;
using RestWithAspNetUdemy.Data.VO;

namespace RestWithAspNetUdemy.Controllers
{
    [ApiVersion("1")]
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
        public IActionResult Signin([FromBody]UserVO userVO)
        {
            if (userVO == null) return BadRequest("Invalid Client Request");

            var token = _loginBusiness.ValidateCredential(userVO);
            if (token == null) return Unauthorized();
            return Ok(token);
        }
    }
}
