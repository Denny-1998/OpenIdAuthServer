namespace OpenIdAuthServer.DTO
{
    public class ClientDTO
    {
        public string ClientId { get; set; } = null!;

        public string ClientSecret { get; set; } = null!;

        public string ClientName { get; set; } = null!;

        public string RedirectUri { get; set; } = null!;

        public string AllowedGrantTypes { get; set; } = null!;

        public string AllowedScopes { get; set; } = null!;
    }
}
