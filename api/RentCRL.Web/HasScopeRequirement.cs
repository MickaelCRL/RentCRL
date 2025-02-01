using Microsoft.AspNetCore.Authorization;
using System;

namespace RentCRL.Web
{
    public class HasScopeRequirement : IAuthorizationRequirement
    {
        public string Issuer { get; }
        public string Scope { get; }
        public HasScopeRequirement(string scope, string issuer)
        {
            Scope = scope ?? throw ArgumentNullException(nameof(scope));
            Issuer = issuer ?? throw ArgumentNullException(nameof(issuer));
        }
    }
}
