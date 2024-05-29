using System;
using System.Collections.Generic;

namespace OpenIdAuthServer.Models;

public partial class Client
{
    public string ClientId { get; set; } = null!;

    public string ClientSecret { get; set; } = null!;

    public string ClientName { get; set; } = null!;

    public string RedirectUri { get; set; } = null!;

    public string AllowedGrantTypes { get; set; } = null!;

    public string AllowedScopes { get; set; } = null!;

    public virtual ICollection<Token> Tokens { get; set; } = new List<Token>();
}
