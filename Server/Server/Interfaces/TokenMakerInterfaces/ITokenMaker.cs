using Microsoft.IdentityModel.Tokens;
using Server.Models;

namespace Server.Interfaces.TokenMakerInterfaces
{
    public interface ITokenMaker
    {
        string CreateToken(User user, SymmetricSecurityKey secretKey);   // User data + secret key for encoding.
    }
}
