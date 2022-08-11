using Server.Enums;

namespace Server.Interfaces.TokenMakerInterfaces
{
    public interface ITokenMakerFactory
    {
        ITokenMaker CreateTokenMaker(ETokenType tokenType);
    }
}
