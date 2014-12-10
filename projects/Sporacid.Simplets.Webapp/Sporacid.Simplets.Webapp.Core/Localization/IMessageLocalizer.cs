namespace Sporacid.Simplets.Webapp.Core.Localization
{
    using System;

    /// <summary>
    /// Interface to resolve application messages in different locale.
    /// </summary>
    /// <author>Simon Turcotte-Langevin</author>
    public interface IMessageLocalizer : IDisposable
    {
        /// <summary>
        /// The default language if we try to get a message without locale.
        /// </summary>
        string DefaultLanguage { get; }

        /// <summary>
        /// Localizes a message associated with a message key into the default locale.
        /// </summary>
        /// <param name="messageKey">The message key to find the corresponding string</param>
        /// <returns>The localized message if found; the message key if not</returns>
        string LocalizeString(string messageKey);

        /// <summary>
        /// Localizes a message associated with a message key into a given locale.
        /// </summary>
        /// <param name="messageKey">The message key to find the corresponding string</param>
        /// <param name="locale">Locale in which to localize</param>
        /// <returns>The localized message if found; the message key if not</returns>
        string LocalizeString(string messageKey, string locale);

        /// <summary>
        /// Localizes and format message associated with a message key into the default locale.
        /// </summary>
        /// <param name="messageKey">The message key to find the corresponding string</param>
        /// <param name="objects">List of objects to insert in the format</param>
        /// <returns>The localized message if found; the message key if not</returns>
        string LocalizeFormatString(string messageKey, params object[] objects);

        /// <summary>
        /// Localizes and format message associated with a message key into a given locale.
        /// </summary>
        /// <param name="messageKey">The message key to find the corresponding string</param>
        /// <param name="locale">Locale in which to localize</param>
        /// <param name="objects">List of objects to insert in the format</param>
        /// <returns>The localized message if found; the message key if not</returns>
        string LocalizeFormatString(string messageKey, string locale, params object[] objects);

        /// <summary>
        /// Returns all the available languages currently in cache.
        /// </summary>
        /// <returns>All available languages</returns>
        string[] GetAvailableLanguages();
    }
}