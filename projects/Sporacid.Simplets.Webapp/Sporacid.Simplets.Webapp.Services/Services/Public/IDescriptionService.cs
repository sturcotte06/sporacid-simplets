namespace Sporacid.Simplets.Webapp.Services.Services.Public
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using Sporacid.Simplets.Webapp.Services.Database.Dto.Description;
    using Sporacid.Simplets.Webapp.Services.Resources.Contracts;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [ContractClass(typeof (DescriptionServiceContract))]
    public interface IDescriptionService : IService
    {
        /// <summary>
        /// Describes the api methods. This can be used to discover what operations are available.
        /// </summary>
        /// <returns>An enumeration of all available api methods.</returns>
        IEnumerable<ApiModuleDescriptionDto> DescribeApiMethods();

        /// <summary>
        /// Describes the api entities. This can be used to discover what entities are used in the api.
        /// </summary>
        /// <returns>An enumeration of all available api entities.</returns>
        IEnumerable<ApiEntityDescriptionDto> DescribeApiEntities();
    }

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [ContractClassFor(typeof (IDescriptionService))]
    internal abstract class DescriptionServiceContract : IDescriptionService
    {
        public IEnumerable<ApiModuleDescriptionDto> DescribeApiMethods()
        {
            // Postconditions.
            Contract.Ensures(Contract.Result<IEnumerable<ApiModuleDescriptionDto>>() != null, ContractStrings.DescriptionService_DescribeApiMethods_EnsuresNonNullModuleDescription);

            // Dummy return.
            return default(IEnumerable<ApiModuleDescriptionDto>);
        }

        public IEnumerable<ApiEntityDescriptionDto> DescribeApiEntities()
        {
            // Postconditions.
            Contract.Ensures(Contract.Result<IEnumerable<ApiEntityDescriptionDto>>() != null, ContractStrings.DescriptionService_DescribeApiEntities_EnsuresNonNullEntityDescriptions);

            // Dummy return.
            return default(IEnumerable<ApiEntityDescriptionDto>);
        }
    }
}