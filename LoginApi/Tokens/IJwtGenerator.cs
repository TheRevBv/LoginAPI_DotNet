using LoginApi.Models;

namespace LoginApi.Tokens;

public interface IJwtGenerator
{
    string CreateToken(User user);
}
