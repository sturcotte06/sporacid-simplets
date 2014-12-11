namespace Sporacid.Simplets.Webapp.Services.Services
{
    using System;
    using PostSharp.Patterns.Contracts;

    /// <summary>
    /// Interface for all membre services.
    /// </summary>
    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public interface IMembreService
    {
        /// <summary>
        /// </summary>
        /// <param name="clubId"></param>
        /// <param name="membre"></param>
        void Add([Positive] Int32 clubId, [Required] Object membre);

        /// <summary>
        /// </summary>
        /// <param name="membreId"></param>
        /// <returns></returns>
        Object Get([Positive] Int32 membreId);
    }
}