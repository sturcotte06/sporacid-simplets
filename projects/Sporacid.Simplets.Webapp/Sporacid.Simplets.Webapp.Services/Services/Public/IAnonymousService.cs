namespace Sporacid.Simplets.Webapp.Services.Services.Public
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using Sporacid.Simplets.Webapp.Core.Security.Authorization;
    using Sporacid.Simplets.Webapp.Services.Database.Dto;
    using Sporacid.Simplets.Webapp.Services.Resources.Contracts;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Module("Default")]
    [FixedContext(SecurityConfig.SystemContext)]
    [ContractClass(typeof (AnonymousServiceContract))]
    public interface IAnonymousService
    {
        /// <summary>
        /// Dummy method that can be called to bootstrap a new user.
        /// </summary>
        [RequiredClaims(Claims.None)]
        void NoOp();

        /// <summary>
        /// Help method to get the api help.
        /// </summary>
        [RequiredClaims(Claims.None)]
        IEnumerable<ApiMethodDescriptionDto> Help();
    }

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [ContractClassFor(typeof (IAnonymousService))]
    internal abstract class AnonymousServiceContract : IAnonymousService
    {
        /// <summary>
        /// Dummy method that can be called to bootstrap a new user.
        /// </summary>
        public void NoOp()
        {
        }

        /// <summary>
        /// Help method to get the api help.
        /// </summary>
        public IEnumerable<ApiMethodDescriptionDto> Help()
        {
            // Postconditions.
            Contract.Ensures(Contract.Result<IEnumerable<ApiMethodDescriptionDto>>() != null, ContractStrings.AnonymousService_Help_EnsuresNonNulApiDescriptor);

            // Dummy return.
            return default(IEnumerable<ApiMethodDescriptionDto>);
        }
    }
}