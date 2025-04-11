using AuthAPI.DTOs;
using AuthAPI.Helper;
using AuthAPI.Models;
using AuthAPI.Repositories;

namespace AuthAPI.Service
{
    public class UserSingUpService
    {
        private readonly IUserRepository _userRepository;
        private readonly IResponseBuilder<User> _responseBuilder;

        public UserSingUpService(IUserRepository userRepository, IResponseBuilder<User> responseBuilder)
        {
            _userRepository = userRepository;
            _responseBuilder = responseBuilder;
        }


        public async Task<ResponseDTO> SingUp(UserDTO userDTO)
        {
            try
            {
                var user = await _userRepository.GetByEmail(userDTO.Email);

                if (user != null)
                {
                    return _responseBuilder.Conflict("O email inserido já possui cadastro.");
                }

                if (userDTO.Password != userDTO.RePassword)
                {
                    return _responseBuilder.Conflict("As senhas inseridas não coincidem, verifique e tente novamente.");
                }

                User newUser = new()
                {
                    UserName = userDTO.UserName,
                    Email = userDTO.Email,
                    Password = userDTO.Password
                };

                await _userRepository.Add(newUser);

                return _responseBuilder.OK(newUser, "Cadastro feito com sucesso.");
            }
            catch (Exception ex)
            {
                return _responseBuilder.InternalError($"Ocorreu um erro interno: {ex}");
            }
        }
    }
}