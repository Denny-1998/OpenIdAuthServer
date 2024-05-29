using System;
using System.Collections.Generic;

namespace OpenIdAuthServer.Models;

public partial class Token
{
    public string Token1 { get; set; } = null!;

    public int UserId { get; set; }

    public string ClientId { get; set; } = null!;

    public string TokenType { get; set; } = null!;

    public string Scope { get; set; } = null!;

    public DateTime ExpiresAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Client Client { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
