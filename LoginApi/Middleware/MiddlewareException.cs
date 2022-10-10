using System.Net;
using System.Runtime.Serialization;

namespace LoginApi.Middleware;

public class MiddlewareException : Exception
{
    public HttpStatusCode Code { get; set; }
    public Object? Errors { get; set; }

    public MiddlewareException(HttpStatusCode code, object? errors = null)
    {
        Code = code;
        Errors = errors;
    }


}
