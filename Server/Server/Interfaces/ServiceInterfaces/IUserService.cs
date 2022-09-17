using Server.Dto.UserDto;
using Server.Models;

namespace Server.Interfaces.ServiceInterfaces
{
    public interface IUserService
    {
        Task<AuthDTO> Login(LoginDTO loginDTO);
        Task<User> RegisterNewUser(RegisterDTO registerDTO);
        Task<User> UpdateUser(UpdateDTO updateDTO);
    }
}
