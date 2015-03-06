namespace Sporacid.Simplets.Webapp.Core.Exceptions.Contracts
{
    using System;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Serializable]
    public class ContractException : CoreException
    {
        public ContractException(String message)
            : base(message)
        {
        }
    }
}