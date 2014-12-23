namespace Sporacid.Simplets.Webapp.Services.Services.Administration
{
    using System;
    using PostSharp.Patterns.Contracts;
    using Sporacid.Simplets.Webapp.Core.Security.Authorization;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Module("Administration")]
    [Contextual("context")]
    public interface IContextAdministrationService
    {
        /// <summary>
        /// Creates a security context in the system.
        /// </summary>
        /// <param name="context">The context.</param>
        [RequiredClaims(Claims.None)] // TODO can this be a DDOS possible attack?
        Int32 CreateContext([Required] String context);

        /// <summary>
        /// Binds a role to the current user.
        /// The role must exists.
        /// If the user is not subscribed to the context, an authorization exception will be raised.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="role">The role.</param>
        /// <param name="identity">The principal identity.</param>
        [RequiredClaims(Claims.Admin | Claims.Update)]
        void BindRoleToPrincipal([Required] String context, [Required] String role, [Required] String identity);

        /// <summary>
        /// Remove all claims from a user on the context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="identity">The principal identity.</param>
        [RequiredClaims(Claims.Admin | Claims.DeleteAll)]
        void RemoveAllClaimsFromPrincipal([Required] String context, [Required] String identity);
    }
}