namespace Sporacid.Simplets.Webapp.Services.Services
{
    using System;
    using PostSharp.Patterns.Contracts;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public interface IContextAdministrationService
    {
        /// <summary>
        /// Binds a role to the current user.
        /// The role must exists.
        /// If the user is not subscribed to the context, an authorization exception will be raised.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="role">The role.</param>
        /// <param name="memberId">The member id.</param>
        void BindRole([Required] String context, [Required] String role, [Positive] int memberId);
    }
}