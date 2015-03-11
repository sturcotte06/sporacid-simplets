namespace Sporacid.Simplets.Webapp.Core.Security.Database
{
    using System;
    using Sporacid.Simplets.Webapp.Core.Repositories;

    /// <summary>
    /// Diregard the name of this file.
    /// This file should be used to store linq to sql corrections to the generated dbml.
    /// </summary>
    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    partial class Claim : IHasId<Int32>
    {
    }

    partial class Module : IHasId<Int32>
    {
    }

    partial class Principal : IHasId<Int32>
    {
    }

    partial class Context : IHasId<Int32>
    {
    }

    partial class RoleTemplate : IHasId<Int32>
    {
    }

    partial class PrincipalModuleContextClaims : IHasId<PrincipalModuleContextClaimsId>
    {
        private PrincipalModuleContextClaimsId id;
        public PrincipalModuleContextClaimsId Id
        {
            get
            {
                return id ?? (id = new PrincipalModuleContextClaimsId
                {
                    PrincipalId = this.PrincipalId,
                    ContextId = this.ContextId,
                    ModuleId = this.ModuleId,
                });
            }
        }
    }

    public class PrincipalModuleContextClaimsId
    {
        public Int32 PrincipalId { get; set; }
        public Int32 ContextId { get; set; }
        public Int32 ModuleId { get; set; }
    }
}