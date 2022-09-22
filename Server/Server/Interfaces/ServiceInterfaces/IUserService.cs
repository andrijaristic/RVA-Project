using Server.Dto.LogsDto;
using Server.Dto.UserDto;
using Server.Models;

namespace Server.Interfaces.ServiceInterfaces
{
    public interface IUserService
    {
        Task<AuthDTO> Login(LoginDTO loginDTO);
        Task<DisplayUserDTO> RegisterNewUser(RegisterDTO registerDTO);
        Task<DisplayUserDTO> UpdateUser(UpdateDTO updateDTO);
        Task<List<LogDTO>> GetLogs(string username);
    }
}
