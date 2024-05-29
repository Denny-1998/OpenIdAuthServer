//using System.Security.Cryptography;
//using OpenIdAuthServer.DTO;
//using OpenIdAuthServer.Models;

//namespace OpenIdAuthServer.Helper
//{
//    public class DatabaseConnection
//    {

//        OidcServerContext _context;

//        public DatabaseConnection(OidcServerContext dbContext)
//        {
//            _context = dbContext;
//        }


//        public User FindUser(string username)
//        {
//            return _context.Users.FirstOrDefault(u => u.Username == username);
//        }



//        public User CreateNewUser(string email, string password)
//        {

//            if (_context.Users.Count(u => u.Email == email) > 0)
//                throw new Exception("User with username " + email + " already exists.");
            

//            byte[] saltAsBytes = RandomNumberGenerator.GetBytes(50);
//            string salt = Convert.ToBase64String(saltAsBytes);

//            HashHelper hs = new HashHelper();
//            string passwordHash = hs.IterateHash(password, salt);

//            User user = new User();
//            user.Username = email;
//            user.Email = email;
//            user.Salt = salt;
//            user.PasswordHash = passwordHash;

//            _context.Users.Add(user);
//            _context.SaveChanges();

//            return user;
//        }



//        public Client GetClient(string clientId)
//        {
//            return _context.Clients.FirstOrDefault(c => c.ClientId == clientId);
//        }

//        public ClientDTO CreateNewClient(string clientName, string redirectUri, string allowedGrantTypes, string allowedScopes)
//        {
//            HashHelper hs = new HashHelper();
            
//            string clientId = hs.ComputeHash_SHA512(RndString(88));
//            string clientSecret = hs.ComputeHash_SHA512(RndString(88));


//            Client client = new Client
//            {
//                ClientId = clientId,
//                ClientSecret = hs.ComputeHash_SHA512(clientSecret),
//                ClientName = clientName,
//                RedirectUri = redirectUri,
//                AllowedGrantTypes = allowedGrantTypes,
//                AllowedScopes = allowedScopes
//            };

//            _context.Clients.Add(client);
//            _context.SaveChanges();

//            ClientDTO clientDTO = new ClientDTO
//            {
//                ClientId = client.ClientId,
//                ClientSecret = clientSecret,
//                ClientName = client.ClientName,
//                RedirectUri = client.RedirectUri,
//                AllowedGrantTypes = client.AllowedGrantTypes,
//                AllowedScopes = client.AllowedScopes
//            };

//            return clientDTO;
//        }

//        private string RndString(int lenght)
//        {
//            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(lenght));
//        }



//    }
//}
