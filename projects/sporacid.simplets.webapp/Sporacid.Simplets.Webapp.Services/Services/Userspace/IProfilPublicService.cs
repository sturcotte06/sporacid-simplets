using Sporacid.Simplets.Webapp.Core.Security.Authorization;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sporacid.Simplets.Webapp.Services.Services.Userspace
{
    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Module("ProfilsPublic")]
    [Contextual("codeUniversel")]
    [ContractClass(typeof(ProfilPublicServiceContract))]
    public interface IProfilPublicService : IService
    {
        /// <summary>
        /// Gets the profilPublic entity from the system.
        /// </summary>
        /// <param name="codeUniversel">The universal code that represents the profil entity.</param>
        /// <returns>The profil.</returns>
        [RequiredClaims(Claims.None)]
        dynamic Get(String codeUniversel);
    }

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [ContractClassFor(typeof(IProfilPublicService))]
    class ProfilPublicServiceContract
    {

    }
}
