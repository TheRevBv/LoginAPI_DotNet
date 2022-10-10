using LoginApi.Dtos.UserDtos;

namespace LoginApi.Database.Users;

public interface IUserRepository
{
    Task<UserResponseDto> GetUser();

    Task<UserResponseDto> Login(UserLoginRequestDto request);

    Task<UserResponseDto> Register(UserRegisterRequestDto request);
}
