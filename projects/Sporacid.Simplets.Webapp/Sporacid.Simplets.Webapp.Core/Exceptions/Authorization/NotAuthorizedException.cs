namespace Sporacid.Simplets.Webapp.Core.Exceptions.Authorization
{
    using System;
    using Sporacid.Simplets.Webapp.Core.Models.Contexts;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class NotAuthorizedException : SecurityException
    {
        public NotAuthorizedException() : base("The action cannot be authorized.")
        {
        }

        public NotAuthorizedException(AuthorizationLevel requiredLevel)
            : base(String.Format("The action cannot be authorized. A minimum authorization level of '{0}' is required to take action.", requiredLevel))
        {
        }

        public NotAuthorizedException(AuthorizationLevel level, AuthorizationLevel requiredLevel)
            : base(String.Format("The action cannot be authorized. A minimum authorization level of '{0}' is required to take action (Current authorization level: {1}).",
                requiredLevel, level))
        {
        }
    }
}