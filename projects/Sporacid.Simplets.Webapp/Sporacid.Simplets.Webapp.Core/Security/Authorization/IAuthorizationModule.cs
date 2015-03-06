namespace Sporacid.Simplets.Webapp.Core.Security.Authorization
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Security.Principal;
    using Sporacid.Simplets.Webapp.Core.Resources.Contracts;
    using Sporacid.Simplets.Webapp.Tools.Strings;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [ContractClass(typeof (AuthorizationModuleContract))]
    public interface IAuthorizationModule
    {
        /// <summary>
        /// Authorizes an authenticated principal for the given module and contexts. If the user is not what
        /// he claims to be, an authorization exception will be raised.
        /// </summary>
        /// <param name="principal">The principal of an authenticated user.</param>
        /// <param name="claims">What the user claims to be authorized to do on the module and context.</param>
        /// <param name="module">The module name which the user tries to access.</param>
        /// <param name="contexts">The context name which the user tries to access.</param>
        void Authorize(IPrincipal principal, Claims claims, String module, params String[] contexts);
    }

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [ContractClassFor(typeof (IAuthorizationModule))]
    internal abstract class AuthorizationModuleContract : IAuthorizationModule
    {
        /// <summary>
        /// Authorizes an authenticated principal for the given module and contexts. If the user is not what
        /// he claims to be, an authorization exception will be raised.
        /// </summary>
        /// <param name="principal">The principal of an authenticated user.</param>
        /// <param name="claims">What the user claims to be authorized to do on the module and context.</param>
        /// <param name="module">The module name which the user tries to access.</param>
        /// <param name="contexts">The context name which the user tries to access.</param>
        public void Authorize(IPrincipal principal, Claims claims, string module, params string[] contexts)
        {
            // Preconditions.
            Contract.Requires(principal != null, ContractStrings.AuthorizationModule_Authorize_RequiresPrincipal);
            Contract.Requires(!String.IsNullOrEmpty(module), ContractStrings.AuthorizationModule_Authorize_RequiresModule);
            Contract.Requires(contexts != null && contexts.Length > 0 && Contract.ForAll(contexts, context => !String.IsNullOrEmpty(context)),
                ContractStrings.AuthorizationModule_Authorize_RequiresContexts);
        }
    }
}