using OpenIdAuthServer.Models;

namespace OpenIdAuthServer.Repository
{
    public class TokenRepository : ITokenRepository
    {
        OidcServerContext _context;


        public TokenRepository() 
        { 
            _context = new OidcServerContext();
        }


        public bool AddToken(Token token)
        {
            _context.Tokens.Add(token);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteToken(string token)
        {
            Token tokenToDelete = _context.Tokens.FirstOrDefault(t => t.Token1 == token);

            _context.Tokens.Remove(tokenToDelete);
            _context.SaveChanges(); 
            return true;
        }

        public Token GetToken(string token)
        {
            Token tokenFromDB = _context.Tokens.FirstOrDefault(t => t.Token1 == token);
            return tokenFromDB;
        }
    }
}
