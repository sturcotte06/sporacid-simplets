namespace Sporacid.Simplets.Webapp.Tools.Strings
{
    using System;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using PostSharp.Patterns.Contracts;

    /// <summary>
    /// Extension method library for strings.
    /// </summary>
    /// <author>Simon Turcotte-Langevin</author>
    public static unsafe class StringExtensions
    {
        /// <summary>
        /// String of all alphanumeric characters.
        /// </summary>
        private const string AlphanumericCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        /// <summary>
        /// New line char cache to prevent creating a new array each
        /// time we call GetLines() on a string.
        /// </summary>
        private static readonly string[] NewLineChars = {"\r\n", "\n"};

        /// <summary>
        /// A secure rng for random number generation.
        /// </summary>
        private static readonly RandomNumberGenerator SecureRng = new RNGCryptoServiceProvider();

        /// <summary>
        /// Generates a secure and random string of the specified length.
        /// </summary>
        /// <param name="length">Length of the string to generate.</param>
        /// <returns></returns>
        public static String SecureRandom(uint length)
        {
            var stringBuilder = new StringBuilder();
            var randomBytes = new byte[4];

            fixed (byte* random = randomBytes)
            {
                for (var i = 0; i < length; i++)
                {
                    // Get the bytes from the secure random rng,
                    SecureRng.GetBytes(randomBytes);

                    // Treat the bytes as an int. Restrict the index to alphanumeric characters length.
                    stringBuilder.Append(AlphanumericCharacters[random[0]%AlphanumericCharacters.Length]);
                }
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Splits a string on line break characters
        /// </summary>
        /// <param name="str">The string to break</param>
        /// <returns>The arary of lines for the string</returns>
        public static string[] GetLines([NotNull] this string str)
        {
            return str.Split(NewLineChars, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Shortcut for the static string.IsNullOrEmpty(). Since this is an extension method,
        /// no null ref exception will be thrown if called on a null string.
        /// </summary>
        /// <param name="str">The string to test</param>
        /// <returns>Whether the string is null or empty.</returns>
        public static bool IsNullOrEmpty([NotNull] this string str)
        {
            return String.IsNullOrEmpty(str);
        }

        /// <summary>
        /// Transforms an hexadecimal string into a sequence of bytes.
        /// </summary>
        /// <param name="hexadecimalString">An hexadecimal string containing only 0-9 and A-F characters</param>
        /// <returns>The byte array corresponding to the the hexadecimal string</returns>
        public static byte[] FromHexString([NotNull] this string hexadecimalString)
        {
            return Enumerable.Range(0, hexadecimalString.Length)
                .Where(x => x%2 == 0)
                .Select(x => Convert.ToByte(hexadecimalString.Substring(x, 2), 16))
                .ToArray();
        }
    }
}