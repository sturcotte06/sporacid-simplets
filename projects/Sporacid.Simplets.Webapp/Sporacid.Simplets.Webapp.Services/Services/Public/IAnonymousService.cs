namespace Sporacid.Simplets.Webapp.Services.Services.Public
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using Sporacid.Simplets.Webapp.Core.Security.Authorization;
    using Sporacid.Simplets.Webapp.Services.Database.Dto.Description;
    using Sporacid.Simplets.Webapp.Services.Resources.Contracts;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Module("Default")]
    [FixedContext(SecurityConfig.SystemContext)]
    [ContractClass(typeof (AnonymousServiceContract))]
    public interface IAnonymousService
    {
        /// <summary>
        /// Dummy method that has no side effect on the system.
        /// However, the request will pass through the api's pipeline; it can therefore be used to test a principal's rights in the
        /// system.
        /// </summary>
        [RequiredClaims(Claims.None)]
        void NoOp();

        /// <summary>
        /// Describes the api methods. This can be used to discover what operations are available.
        /// </summary>
        /// <returns>An enumeration of all available api methods.</returns>
        [RequiredClaims(Claims.None)]
        IEnumerable<ApiMethodDescriptionDto> DescribeApiMethods();

        /// <summary>
        /// Describes the api entities. This can be used to discover what entities are used in the api.
        /// </summary>
        /// <returns>An enumeration of all available api entities.</returns>
        [RequiredClaims(Claims.None)]
        IEnumerable<ApiEntityDescriptionDto> DescribeApiEntities();
    }

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [ContractClassFor(typeof (IAnonymousService))]
    internal abstract class AnonymousServiceContract : IAnonymousService
    {
        public void NoOp()
        {
        }

        public IEnumerable<ApiMethodDescriptionDto> DescribeApiMethods()
        {
            // Postconditions.
            Contract.Ensures(Contract.Result<IEnumerable<ApiMethodDescriptionDto>>() != null, ContractStrings.AnonymousService_DescribeApiMethods_EnsuresNonNulApiDescriptor);

            // Dummy return.
            return default(IEnumerable<ApiMethodDescriptionDto>);
        }

        public IEnumerable<ApiEntityDescriptionDto> DescribeApiEntities()
        {
            // Postconditions.
            Contract.Ensures(Contract.Result<IEnumerable<ApiEntityDescriptionDto>>() != null, ContractStrings.AnonymousService_DescribeApiEntities_EnsuresNonNulApiDescriptor);

            // Dummy return.
            return default(IEnumerable<ApiEntityDescriptionDto>);
        }
    }
}