using AuthAPI.DTOs;

namespace AuthAPI.Helper
{
    public abstract class ResponseBase<T> : IResponseBuilder<T> where T : class
    {
        public abstract ResponseDTO Conflict(string message);

        public abstract ResponseDTO InternalError(string message);

        public abstract ResponseDTO NotFound(string message);

        public abstract ResponseDTO OK(T date, string message);
    }
}