using System.Security.Cryptography;
using OpenIdAuthServer.DTO;
using OpenIdAuthServer.Models;


namespace OpenIdAuthServer.Repository
{
    public class ClientRepository : IClientRepository
    {

        OidcServerContext _context;

        public ClientRepository()
        {
            _context = new OidcServerContext();
        }




        public Client GetClient(string clientId)
        {
            return _context.Clients.FirstOrDefault(c => c.ClientId == clientId);
        }

        public ClientDTO CreateNewClient(string clientName, string redirectUri, string allowedGrantTypes, string allowedScopes)
        {
            HashHelper hs = new HashHelper();

            string clientId = hs.ComputeHash_SHA512(RndString(88));
            string clientSecret = hs.ComputeHash_SHA512(RndString(88));


            Client client = new Client
            {
                ClientId = clientId,
                ClientSecret = hs.ComputeHash_SHA512(clientSecret),
                ClientName = clientName,
                RedirectUri = redirectUri,
                AllowedGrantTypes = allowedGrantTypes,
                AllowedScopes = allowedScopes
            };

            _context.Clients.Add(client);
            _context.SaveChanges();

            ClientDTO clientDTO = new ClientDTO
            {
                ClientId = client.ClientId,
                ClientSecret = clientSecret,
                ClientName = client.ClientName,
                RedirectUri = client.RedirectUri,
                AllowedGrantTypes = client.AllowedGrantTypes,
                AllowedScopes = client.AllowedScopes
            };

            return clientDTO;
        }

        public Client UpdateClient(Client client)
        {
            throw new NotImplementedException();
        }

        public void DeleteClient(string clientId)
        {
            throw new NotImplementedException();
        }


        private string RndString(int lenght)
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(lenght));
        }

    }
}
