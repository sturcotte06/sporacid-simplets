namespace Sporacid.Simplets.Webapp.Services.Services.Public
{
  using System;
  using System.Diagnostics.Contracts;
  using Sporacid.Simplets.Webapp.Core.Security.Authorization;
  using Sporacid.Simplets.Webapp.Services.Resources.Contracts;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Module("Default")]
    [FixedContext(SecurityConfig.SystemContext)]
    [ContractClass(typeof (AnonymousServiceContract))]
    public interface IAnonymousService : IService
    {
        /// <summary>
        /// Dummy method that has no side effect on the system.
        /// However, the request will pass through the api's pipeline; it can therefore be used to test a principal's rights in the
        /// system.
        /// </summary>
        [RequiredClaims(Claims.None)]
        void NoOp();

        /// <summary>
        /// Create an empty dto object from the given entity name.
        /// </summary>
        /// <param name="entityName">Entity name without "Dto" part.</param>
        /// <returns>An empty dto object from the given entity name.</returns>
        [RequiredClaims(Claims.None)]
        dynamic Empty(string entityName);
    }

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [ContractClassFor(typeof (IAnonymousService))]
    internal abstract class AnonymousServiceContract : IAnonymousService
    {
        public void NoOp()
        {
        }

        public dynamic Empty(string entityName)
        {
          // Preconditions.
          Contract.Requires(!String.IsNullOrEmpty(entityName), ContractStrings.AnonymousService_Empty_RequiresEntityName);

          // Postconditions.
          Contract.Ensures(Contract.Result<dynamic>() != null,
              ContractStrings.AnonymousService_Empty_EnsuresNonNullEntity);

          return default(dynamic);
        }
    }
}