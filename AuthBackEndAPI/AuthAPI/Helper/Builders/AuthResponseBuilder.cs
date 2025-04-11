using AuthAPI.DTOs;
using AuthAPI.Models;

namespace AuthAPI.Helper.Builders
{
    public class AuthResponseBuilder : ResponseBase<User>, IResponseBuilder<User>
    {
        public override ResponseDTO Conflict(string message)
        {

            ResponseDTO response = new()
            {
                Message = message,
                Success = false,
            };

            return response;
        }

        public override ResponseDTO InternalError(string message)
        {
            ResponseDTO response = new()
            {
                Message = message,
                Success = false,
            };

            return response;
        }

        public override ResponseDTO NotFound(string message)
        {
            ResponseDTO response = new()
            {
                Message = message,
                Success = false,
            };

            return response;
        }

        public override ResponseDTO OK(User date, string message)
        {
            ResponseDTO response = new()
            {
                Message = message,
                Success = true,
                Date = date
            };

            return response;
        }
    }
}