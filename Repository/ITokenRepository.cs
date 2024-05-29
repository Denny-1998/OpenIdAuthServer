using OpenIdAuthServer.Models;

namespace OpenIdAuthServer.Repository
{
    public interface ITokenRepository
    {

        public bool AddToken(Token token);

        public bool DeleteToken(string token);

        public Token GetToken(string token);
    }
}
