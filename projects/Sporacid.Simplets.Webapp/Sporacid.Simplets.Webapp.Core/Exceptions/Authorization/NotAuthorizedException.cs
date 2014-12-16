namespace Sporacid.Simplets.Webapp.Core.Exceptions.Authorization
{
    using System;
    using Sporacid.Simplets.Webapp.Core.Security.Authorization;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class NotAuthorizedException : SecurityException
    {
        public NotAuthorizedException() : base("The action cannot be authorized.")
        {
        }

        public NotAuthorizedException(IResource resource)
            : base(String.Format("The action cannot be authorized for resource '{0}'.", resource.Value))
        {
        }
    }
}