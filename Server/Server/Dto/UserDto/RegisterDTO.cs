using Server.Enums;

namespace Server.Dto.UserDto
{
    public class RegisterDTO
    {
        public string Username { get;set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }
}
