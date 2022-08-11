using Microsoft.IdentityModel.Tokens;
using Server.Interfaces.TokenMakerInterfaces;
using Server.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Server.TokenMakers
{
    public class JWTMaker : ITokenMaker
    {
        public string CreateToken(User user, SymmetricSecurityKey secretKey)
        {
            List<Claim> claims = new List<Claim>();

            if (user.UserType == Enums.EUserType.ADMIN)
                claims.Add(new Claim(ClaimTypes.Role, "admin"));

            if (user.UserType == Enums.EUserType.STUDENT)
                claims.Add(new Claim(ClaimTypes.Role, "student"));

            claims.Add(new Claim("Sys_user", "logged_in")); // Validation that User is logged in.
            claims.Add(new Claim(ClaimTypes.Name, user.Username));  // Saving username in case accidental change happens.

            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenOptions = new JwtSecurityToken
            (
                issuer: "http://localhost:44398",
                claims: claims,
                expires: DateTime.Now.AddMinutes(45),
                signingCredentials: signinCredentials
            );

            string tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return tokenString;
        }
    }
}
