using Server.Enums;
using Server.Interfaces.TokenMakerInterfaces;

namespace Server.TokenMakers
{
    public class TokenMakerFactory : ITokenMakerFactory
    {
        public ITokenMaker CreateTokenMaker(ETokenType tokenType)
        {
            if (tokenType == ETokenType.JWT)
            {
                return new JWTMaker();
            }

            return null;
        }
    }
}
