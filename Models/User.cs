using System;
using System.Collections.Generic;

namespace OpenIdAuthServer.Models;

public partial class User
{
    public string Email { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public int UserId { get; set; }

    public string Salt { get; set; } = null!;

    public virtual ICollection<Token> Tokens { get; set; } = new List<Token>();
}
