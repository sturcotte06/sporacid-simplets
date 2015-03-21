namespace Sporacid.Simplets.Webapp.Core.Security.Events
{
    using Sporacid.Simplets.Webapp.Core.Events.Impl;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class PrincipalAuthorized : Event<PrincipalAuthorizedEventArgs>
    {
        public PrincipalAuthorized(object sender, PrincipalAuthorizedEventArgs eventArgs) : base(sender, eventArgs)
        {
        }
    }

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class PrincipalAuthorizedEventArgs
    {

    }
}