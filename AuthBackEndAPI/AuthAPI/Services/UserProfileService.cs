using AuthAPI.DTOs;
using AuthAPI.Repositories;

namespace AuthAPI.Service
{
    public class UserProfileService
    {
        private readonly IUserRepository _userRepository;
        public UserProfileService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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