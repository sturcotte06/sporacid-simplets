namespace Sporacid.Simplets.Webapp.Core.Security.Authorization.Impl
{
    using System.Security.Claims;
    using System.Security.Principal;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class ClaimsPrincipal : System.Security.Claims.ClaimsPrincipal
    {
        internal ClaimsPrincipal(IPrincipal basePrincipal, params Claim[] claims)
        {
            this.UpdradePrincipal(basePrincipal, claims);
        }

        /// <summary>
        /// Updgrade the original principal to a claim-based principal.
        /// </summary>
        private void UpdradePrincipal(IPrincipal basePrincipal, params Claim[] claims)
        {
            var claimsIdentity = new ClaimsIdentity(basePrincipal.Identity);
            foreach (var claim in this.Claims)
            {
                claimsIdentity.AddClaim(claim);
            }

            this.AddIdentity(claimsIdentity);
        }
    }
}