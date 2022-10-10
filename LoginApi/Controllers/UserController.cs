using Microsoft.AspNetCore.Mvc;
using LoginApi.Database.Users;
using LoginApi.Dtos.UserDtos;
using Microsoft.AspNetCore.Authorization;

namespace LoginApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;

        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserResponseDto>> Login([FromBody] UserLoginRequestDto request)
        {
            return await _repository.Login(request);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<UserResponseDto>> Register([FromBody] UserRegisterRequestDto request)
        {
            return await _repository.Register(request);
        }

        [HttpGet]
        public async Task<ActionResult<UserResponseDto>> GetUsuario()
        {
            return await _repository.GetUser();
        }
    }
}
