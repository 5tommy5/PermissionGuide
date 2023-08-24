using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PermissionApi.Models;
using PermissionApi.Services;

namespace PermissionApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _service;
        public AuthController(AuthService service)
        {
            _service = service;
        }

        [HttpPost("sign-in")]
        public Task<TokenModel> SingIn(SignInModel model)
        {
            return _service.SignIn(model);
        }

        [HttpPost("sign-up")]
        public Task<TokenModel> SingUp(SignUpModel model)
        {
            return _service.SignUp(model);
        }
    }
}
