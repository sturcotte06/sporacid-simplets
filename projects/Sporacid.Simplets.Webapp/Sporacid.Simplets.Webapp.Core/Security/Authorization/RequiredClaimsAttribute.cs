namespace Sporacid.Simplets.Webapp.Core.Security.Authorization
{
    using System;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class RequiredClaimsAttribute : Attribute
    {
        public RequiredClaimsAttribute(Claims claims)
        {
            this.RequiredClaims = claims;
        }

        /// <summary>
        /// Required claims for taking action.
        /// </summary>
        public Claims RequiredClaims { get; private set; }
    }
}