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


        public async Task<ResponseDTO> SingUp(UserDTO userDTO)
        {
            ResponseDTO response = new() { };
            User newUser = new() { };

            try
            {
                var user = await _userRepository.GetByEmail(userDTO.Email);

                if (user != null)
                {
                    response = new()
                    {
                        Message = "Informações inseridas estão não são valídas!",
                        Success = false,
                        Date = null
                    };

                    return response;
                }

                if (userDTO.Password != userDTO.RePassword)
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

                return response = new()
                {
                    Message = "Usuário criado com sucesso!",
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

        public async Task<ResponseDTO> SingIn(UserDTO userDTO)
        {
            Session session = new() { };
            ResponseDTO response = new();
            UserLoginDTO userLogin = new();

            try
            {
                var user = await _userRepository.GetByEmail(userDTO.Email);

                if (user == null)
                {
                    response = new()
                    {
                        Message = "Usuario não encontrado!",
                        Success = false,
                        Date = null
                    };

                    return response;
                }

                if (userDTO.Password != userDTO.RePassword)
                {
                    response = new()
                    {
                        Message = "Senhas inseridas não coincidem!",
                        Success = false,
                        Date = user
                    };

                    return response;
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

                return response = new()
                {
                    Message = "Login feito com sucesso!",
                    Success = true,
                    Date = userLogin,
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

        public async Task<ResponseDTO> DeleteUser(Guid id)
        {
            ResponseDTO response = new();

            try
            {
                var user = await _userRepository.GetById(id);

                if (user == null)
                {
                    return new ResponseDTO
                    {
                        Success = false,
                        Message = "Usuario não encontrado!",
                        Date = null
                    };
                }

                await _userRepository.Delete(user);

                return new ResponseDTO
                {
                    Success = true,
                    Message = "Usuario deletado com sucesso!",
                    Date = user
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