namespace Sporacid.Simplets.Webapp.Core.Models.Contexts
{
    using System;

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