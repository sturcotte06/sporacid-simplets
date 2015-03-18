namespace Sporacid.Simplets.Webapp.Services.Services.Public
{
    using System.Diagnostics.Contracts;
    using Sporacid.Simplets.Webapp.Core.Security.Authorization;

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
    }

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [ContractClassFor(typeof (IAnonymousService))]
    internal abstract class AnonymousServiceContract : IAnonymousService
    {
        public void NoOp()
        {
        }
    }
}