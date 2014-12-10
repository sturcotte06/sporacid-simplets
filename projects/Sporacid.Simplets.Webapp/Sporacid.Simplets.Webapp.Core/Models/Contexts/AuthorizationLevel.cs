namespace Sporacid.Simplets.Webapp.Core.Models.Contexts
{
    using System;

    public enum AuthorizationLevel
    {
        Anonymous,
        Authenticated,
        Superuser,
        Administrator
    }
}