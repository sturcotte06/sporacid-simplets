namespace Sporacid.Simplets.Webapp.Services.Services.Security
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Web;
    using Sporacid.Simplets.Webapp.Core.Exceptions;
    using Sporacid.Simplets.Webapp.Core.Exceptions.Repositories;
    using Sporacid.Simplets.Webapp.Core.Security.Authorization;
    using Sporacid.Simplets.Webapp.Services.Resources.Contracts;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Module("Contexts")]
    [Contextual("context")]
    [ContractClass(typeof (ContextServiceContract))]
    public interface IContextService : IService
    {
        /// <summary>
        /// Returns all claims of the current user, by module, on the given context.
        /// </summary>
        /// <param name="context">The context name.</param>
        /// <exception cref="RepositoryException">
        /// If something unexpected occurs while getting all claims.
        /// </exception>
        /// <exception cref="CoreException">
        /// If something unexpected occurs.
        /// </exception>
        /// <returns>A dictionary of all claims, by module.</returns>
        [RequiredClaims(Claims.ReadAll)]
        IEnumerable<KeyValuePair<String, Claims>> GetAllClaimsOnContext(String context);
    }

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [ContractClassFor(typeof (IContextService))]
    internal abstract class ContextServiceContract : IContextService
    {
        public IEnumerable<KeyValuePair<String, Claims>> GetAllClaimsOnContext(String context)
        {
            // Preconditions.
            Contract.Requires(HttpContext.Current.User != null, ContractStrings.ContextService_GetAllClaimsOnContext_RequiresAuthenticatedPrincipal);
            Contract.Requires(!String.IsNullOrEmpty(context), ContractStrings.ContextService_GetAllClaimsOnContext_RequiresContext);

            // Postconditions.
            Contract.Ensures(Contract.Result<IEnumerable<KeyValuePair<String, Claims>>>() != null, ContractStrings.ContextService_GetAllClaimsOnContext_EnsuresNonNullClaimsByModule);

            // Dummy return.
            return default(IEnumerable<KeyValuePair<String, Claims>>);
        }
    }
}