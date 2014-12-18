namespace Sporacid.Simplets.Webapp.Core.Security.Authorization
{
    using System;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Flags]
    public enum Claims
    {
        None = 0,
        Create = 1,
        CreateAll = 2,
        Update = 4,
        UpdateAll = 8,
        Delete = 16,
        DeleteAll = 32,
        Read = 64,
        ReadAll = 128,
        Admin = 256
    }
}