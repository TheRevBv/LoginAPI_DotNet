using System.Net;
using LoginApi.Dtos.UserDtos;
using LoginApi.Middleware;
using LoginApi.Models;
using LoginApi.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LoginApi.Database.Users;

public class UserRepository : IUserRepository
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IJwtGenerator _jwtGenerator;
    private readonly DataContext _context;
    private readonly IUserSession _userSession;

    public UserRepository(UserManager<User> userManager, SignInManager<User> signInManager, IJwtGenerator jwtGenerator,
        DataContext context, IUserSession userSession)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtGenerator = jwtGenerator;
        _context = context;
        _userSession = userSession;
    }

    private UserResponseDto TransformerUserToUserDto(User user)
    {
        return new UserResponseDto
        {
            Id = user.Id,
            Name = user.Name,
            UserName = user.UserName,
            Token = _jwtGenerator.CreateToken(user)
        };
    }

    public async Task<UserResponseDto> GetUser()
    {
        var user = await _userManager.FindByNameAsync(_userSession.GetUserSession());
        if (user is null)
        {
            throw new MiddlewareException(
                HttpStatusCode.Unauthorized,
                new {msj = "Usuario no encontrado"}
                );
        }
        return TransformerUserToUserDto(user!);
    }

    public async Task<UserResponseDto> Login(UserLoginRequestDto request)
    {
        var usuario = await _userManager.FindByNameAsync(request.UserName!);
        if (usuario is null)
        {
            throw new MiddlewareException(
                HttpStatusCode.Unauthorized,
                new { msj = "Usuario no encontrado en la base de datos" }
            );
        }
        var result = await _signInManager.CheckPasswordSignInAsync(usuario!, request.Password!, false);

        if (result.Succeeded)
        {
            return TransformerUserToUserDto(usuario!);
        }
        throw new MiddlewareException(HttpStatusCode.Unauthorized, new { msj = "Las credenciales son incorrectas" });
    }

    public async Task<UserResponseDto> Register(UserRegisterRequestDto request)
    {
        var existUser = await _context.Users.Where(x => x.UserName == request.UserName).AnyAsync();
        if (existUser)
        {
            throw new MiddlewareException(HttpStatusCode.BadRequest, new { msj = "El usuario ya existe" });
        }
        var hash = new HashPassword();
        var usuario = new User
        {
            Name = request.Name,
            UserName = request.UserName,
        };
        // var result = _userManager.CreateAsync(usuario!, hash.HashearPassword(request.Password!));
        var result = _userManager.CreateAsync(usuario!, request.Password);
        if (result.Result.Succeeded)
        {
            return TransformerUserToUserDto(usuario);
        }
        throw new Exception("No se pudo registrar el usuario");
    }
}
