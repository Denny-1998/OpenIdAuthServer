using System.Security.Cryptography;
using OpenIdAuthServer.DTO;
using OpenIdAuthServer.Models;

namespace OpenIdAuthServer.Repository
{
    public class UserRepository : IUserRepository
    {

        OidcServerContext _context;

        public UserRepository()
        {
            _context = new OidcServerContext();
        }


        public User FindUser(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username);
        }

        public User FindUserById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.UserId == id);
        }



        public User CreateNewUser(string email, string password)
        {

            if (_context.Users.Count(u => u.Email == email) > 0)
                throw new Exception("User with username " + email + " already exists.");


            byte[] saltAsBytes = RandomNumberGenerator.GetBytes(50);
            string salt = Convert.ToBase64String(saltAsBytes);

            HashHelper hs = new HashHelper();
            string passwordHash = hs.IterateHash(password, salt);

            User user = new User();
            user.Username = email;
            user.Email = email;
            user.Salt = salt;
            user.PasswordHash = passwordHash;

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }



        public User UpdateUser(User user)
        {
            throw new NotImplementedException();
        }

        public User DeleteUser(string username)
        {
            throw new NotImplementedException();
        }
    }
}
