using OpenIdAuthServer.Models;
using OpenIdAuthServer.Repository;

namespace OpenIdAuthServer.Helper
{
    public class LoggedInUser
    {

        string _username, _password;
        IUserRepository _userRepository;
        HashHelper _hashHelper;
        User _user;

        public LoggedInUser(string username, string password, IUserRepository userRepository)
        {
            _username = username;
            _password = password;

            _hashHelper = new HashHelper();
            _userRepository = userRepository;
        }

        public bool checkUsername()
        {
            _user = _userRepository.FindUser(_username);
            if (_user == null)
            {
                //calculate fake hash for same time delay 
                _hashHelper.IterateHash(_password, "dummy salt");
                return false;
            }
            return true;
        }

        public bool checkPassword()
        {

            string salt = _user.Salt;

            string hash = _hashHelper.IterateHash(_password, salt);


            if (hash == _user.PasswordHash)
                return true;


            return false;
        }
    }
}
