using AuthAPI.DTOs;
using AuthAPI.Helper;
using AuthAPI.Models;
using AuthAPI.Repositories;

namespace AuthAPI.Service
{
    public class UserProfileService
    {
        private readonly IUserRepository _userRepository;
        private readonly IResponseBuilder<User> _responseBuilder;
        public UserProfileService(IUserRepository userRepository, IResponseBuilder<User> responseBuilder)
        {
            _userRepository = userRepository;
            _responseBuilder = responseBuilder;
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

        public async Task<ResponseDTO> ChangeUserInformation(Guid id, ChengeAccountDTO chengeAccountDTO)
        {

            try
            {
                var user = await _userRepository.GetById(id);

                if (user == null)
                {
                    return _responseBuilder.NotFound("Usuario não encontrado, Verifique e tente novamente.");
                }

                if (chengeAccountDTO.Password != chengeAccountDTO.RePassword)
                {
                    return _responseBuilder.Conflict("As senhas inseridas não coincidem, verifique e tente novamente.");
                }

                user.UserName = chengeAccountDTO.UserName;
                user.Password = chengeAccountDTO.Password;

                await _userRepository.Change();

                return _responseBuilder.OK(null, "Informações alteradas com sucesso!");
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