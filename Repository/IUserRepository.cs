using OpenIdAuthServer.Models;

namespace OpenIdAuthServer.Repository
{
    public interface IUserRepository
    {

        public User FindUser(string username);
        public User FindUserById(int id);


        public User CreateNewUser(string email, string password);

        public User UpdateUser(User user);

        public User DeleteUser(string username);


    }
}
