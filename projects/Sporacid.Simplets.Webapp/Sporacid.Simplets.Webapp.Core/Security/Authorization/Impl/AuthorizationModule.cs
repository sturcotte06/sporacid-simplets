namespace Sporacid.Simplets.Webapp.Core.Security.Authorization.Impl
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Security.Principal;
    using Sporacid.Simplets.Webapp.Core.Exceptions.Authorization;
    using Sporacid.Simplets.Webapp.Core.Repositories;
    using Sporacid.Simplets.Webapp.Tools.Collections;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class AuthorizationModule : IAuthorizationModule
    {
        private readonly IRepository<Int32, Database.Principal> principalRepository;
        private readonly IRepository<Int32, Database.Resource> resourceRepository;

        public AuthorizationModule(IRepository<Int32, Database.Principal> principalRepository, IRepository<Int32, Database.Resource> resourceRepository)
        {
            this.principalRepository = principalRepository;
            this.resourceRepository = resourceRepository;
        }

        /// <summary>
        /// Authorizes an authenticated session in the given context. If the session, and its associated user, does
        /// not have the required authorization level, an exception will be raised.
        /// </summary>
        /// <param name="principal">The principal of an authenticated user.</param>
        /// <param name="resource">The resource the user tries to access.</param>
        public IPrincipal Authorize(IPrincipal principal, IResource resource)
        {
            var resourceEntity = this.resourceRepository.GetUnique(r => r.Name == resource.Value);
            var resourceRequiredClaims = resourceEntity.ResourceRequiredClaims
                .Select(rrc => rrc.Claim)
                .ToArray();

            var principalEntity = this.principalRepository.GetUnique(p => p.Identity == principal.Identity.Name);
            var principalClaimsOnResource = principalEntity.PrincipalResourceClaims
                .Where(prc => prc.Resource.Name == resource.Value)
                .Select(prc => prc.Claim)
                .ToArray();

            // Assert that all required claims for this resource are possessed by the principal.
            resourceRequiredClaims.ForEach(requiredClaim =>
            {
                if (principalClaimsOnResource.All(c => c != requiredClaim))
                {
                    // The principal lacks a claim.
                    throw new NotAuthorizedException();
                }
            });

            // Upgrade principal with the claims.
            var upgradedPrincipal = new ClaimsPrincipal(principal,
                principalEntity.PrincipalResourceClaims.Select(prc => new Claim(prc.Resource.Name, prc.Claim.Name)).ToArray());

            return upgradedPrincipal;
        }
    }
}