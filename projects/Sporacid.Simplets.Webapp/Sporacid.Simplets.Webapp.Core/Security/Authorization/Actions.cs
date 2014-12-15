namespace Sporacid.Simplets.Webapp.Core.Security.Authorization
{
    using System;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Flags]
    public enum Actions
    {
        Create,
        CreateAll,
        Read,
        ReadAll,
        Update,
        UpdateAll,
        Delete,
        DeleteAll
    }
}