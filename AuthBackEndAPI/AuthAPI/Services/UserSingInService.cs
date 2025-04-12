using AuthAPI.DTOs;
using AuthAPI.Helper;
using AuthAPI.Models;
using AuthAPI.Repositories;

namespace AuthAPI.Service
{
    public class UserSingInService
    {
        private readonly IUserRepository _userRepository;
        private readonly ISessionRepository _sessionRepository;
        private readonly IResponseBuilder<UserLoginDTO> _responseBuilder;

        public UserSingInService(IUserRepository userRepository, ISessionRepository sessionRepository, IResponseBuilder<UserLoginDTO> responseBuilder)
        {
            _userRepository = userRepository;
            _responseBuilder = responseBuilder;
            _sessionRepository = sessionRepository;
        }


        public async Task<ResponseDTO> SingIn(UserDTO userDTO)
        {
            Session session = new() { };
            UserLoginDTO userLogin = new();

            try
            {
                var user = await _userRepository.GetByEmail(userDTO.Email);

                if (user == null)
                {
                    return _responseBuilder.NotFound("Email enserido não encontrado, Verifique e tente novamente.");
                }

                if (userDTO.Password != userDTO.RePassword)
                {
                    return _responseBuilder.Conflict("As senhas inseridas não coincidem, verifique e tente novamente.");
                }

                var token = TokenService.GenerateToken(user);

                session = new()
                {
                    UserId = user.Id,
                    Token = token
                };

                await _sessionRepository.Add(session);

                userLogin = new()
                {
                    Id = user.Id,
                    Email = user.Email,
                    UserName = user.UserName,
                    Session = user.Session,
                    CreatedAt = user.CreatedAt,
                    UpdatedAt = user.UpdatedAt
                };

                return _responseBuilder.OK(userLogin, "Usuário foi logado com sucesso!");
            }
            catch (Exception ex)
            {
                return _responseBuilder.InternalError($"Ocorreu um erro interno: {ex}");
            }
        }
    }
}