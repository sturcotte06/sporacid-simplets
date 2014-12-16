namespace Sporacid.Simplets.Webapp.Core.Security.Authentication.Tokens.Impl
{
    using System;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class AuthenticationToken : IToken
    {
        /// <summary>
        /// The token's key.
        /// </summary>
        public string Key { get; internal set; }

        /// <summary>
        /// Utc time at which the token was generated.
        /// </summary>
        public DateTime EmittedAt { get; internal set; }

        /// <summary>
        /// The time span for which the token is valid.
        /// </summary>
        public TimeSpan ValidFor { get; internal set; }

        /// <summary>
        /// Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object" />.
        /// </returns>
        public override int GetHashCode()
        {
            // Only the key should be used for hash code.
            return this.Key.GetHashCode();
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />.
        /// </summary>
        /// <returns>
        /// true if the specified object  is equal to the current object; otherwise, false.
        /// </returns>
        /// <param name="obj">The object to compare with the current object. </param>
        public override bool Equals(object obj)
        {
            var token = obj as IToken;
            return token != null && this.Key.Equals(token.Key);
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return String.Format("{0} {1} {2}", this.Key, this.EmittedAt, this.ValidFor);
        }
    }
}