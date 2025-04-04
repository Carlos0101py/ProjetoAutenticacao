using AuthAPI.Repositories;
using AuthAPI.Models;
using AuthAPI.DTOs;

namespace AuthAPI.Service
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        private readonly SessionRepository _sessionRepository;

        public UserService(UserRepository userRepository, SessionRepository sessionRepository)
        {
            _userRepository = userRepository;
            _sessionRepository = sessionRepository;
        }


        public async Task<ResponseDTO> Login(UserDTO userDTO)
        {
            ResponseDTO response = new() { };
            User newUser = new() { };
            Session session = new() { };

            try
            {
                var user = await _userRepository.GetByEmail(userDTO.Email);

                if (user != null)
                {
                    response = new()
                    {
                        Message = "Informações inseridas estão não são valídas!",
                        Success = false,
                        Date = user
                    };

                    return response;
                }

                if(userDTO.Password != userDTO.RePassword)
                {
                    response = new()
                    {
                        Message = "Senhas inseridas não coincidem!",
                        Success = false,
                        Date = user
                    };

                    return response;
                }

                newUser = new()
                {
                    UserName = userDTO.UserName,
                    Email = userDTO.Email,
                    Password = userDTO.Password
                };

                await _userRepository.Add(newUser);
                var token = TokenService.GenerateToken(newUser);

                session = new()
                {
                    Token = token,
                    UserId = newUser.Id
                };

                await _sessionRepository.Add(session);
                
                return response = new()
                {
                    Message = "Usuario Cadastrado com sucesso!",
                    Success = true,
                    Date = newUser
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    Success = false,
                    Message = $"Ocorreu um erro interno: {ex.Message}",
                    Date = null
                };
            }
        }
    }
}