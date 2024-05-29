using OpenIdAuthServer.DTO;
using OpenIdAuthServer.Models;

namespace OpenIdAuthServer.Repository
{
    public interface IClientRepository
    {

        public Client GetClient(string clientId);

        public ClientDTO CreateNewClient(string clientName, string redirectUri, string allowedGrantTypes, string allowedScopes);

        public Client UpdateClient(Client client);

        public void DeleteClient(string clientId);
    }
}
