using AuthAPI.DTOs;

namespace AuthAPI.Helper
{
    public interface IResponseBuilder<T>
    {
        ResponseDTO OK(T date, string message);
        ResponseDTO Conflict(string message);
        ResponseDTO NotFound(string message);
        ResponseDTO InternalError(string message);
    }   
}