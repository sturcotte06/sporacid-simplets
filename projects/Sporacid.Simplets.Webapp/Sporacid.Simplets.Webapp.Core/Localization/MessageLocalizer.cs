namespace Sporacid.Simplets.Webapp.Core.Localization
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Xml;
    using System.Xml.Linq;
    using log4net;

    /// <summary>
    /// IMessageLocalizer implementation to resolve application messages in different locale.
    /// Based on Réal Forté's design.
    /// </summary>
    /// <author>Simon Turcotte-Langevin</author>
    public class MessageLocalizer : IMessageLocalizer
    {
        /// <summary>
        /// Log4Net logger
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The dictionary for all application's messages.
        /// The first dictionary sorts them by locale,
        /// the second dictionary sorts them by message keys.
        /// We don't need a thread-safe collection, because once a 
        /// MessageLocalizer is instantiated, no more message can be added.
        /// </summary>
        private readonly IDictionary<string, IDictionary<string, string>> applicationMessages = new Dictionary<string, IDictionary<string, string>>();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="messageFilePaths">Paths to the message definition xml files</param>
        public MessageLocalizer(IEnumerable<string> messageFilePaths)
        {
            this.LoadApplicationMessages(messageFilePaths);
        }

        /// <summary>
        /// The default language if we try to get a message without locale.
        /// </summary>
        public string DefaultLanguage { get; set; }

        /// <summary>
        /// Destructor.
        /// </summary>
        public void Dispose()
        {
            this.applicationMessages.Clear();
        }

        /// <summary>
        /// Localizes a message associated with a message key into the default locale.
        /// </summary>
        /// <param name="messageKey">The message key to find the corresponding string</param>
        /// <returns>The localized message if found; the message key if not</returns>
        public string LocalizeString(string messageKey)
        {
            return this.LocalizeString(messageKey, this.DefaultLanguage);
        }

        /// <summary>
        /// Localizes a message associated with a message key into a given locale.
        /// </summary>
        /// <param name="messageKey">The message key to find the corresponding string</param>
        /// <param name="locale">Locale in which to localize</param>
        /// <returns>The localized message if found; the message key if not</returns>
        public string LocalizeString(string messageKey, string locale)
        {
            // Default behaviour: return the key if no associed message were found
            var localizedMessage = messageKey;
            IDictionary<String, String> messagesDict;

            if (locale == null)
                locale = this.DefaultLanguage;

            if (this.applicationMessages.TryGetValue(locale, out messagesDict) && messagesDict.ContainsKey(messageKey))
            {
                messagesDict.TryGetValue(messageKey, out localizedMessage);
            }

            return localizedMessage;
        }

        /// <summary>
        /// Localizes and format message associated with a message key into the default locale.
        /// </summary>
        /// <param name="messageKey">The message key to find the corresponding string</param>
        /// <param name="objects">List of objects to insert in the format</param>
        /// <returns>The localized message if found; the message key if not</returns>
        public string LocalizeFormatString(string messageKey, params object[] objects)
        {
            return this.LocalizeFormatString(messageKey, this.DefaultLanguage, objects);
        }

        /// <summary>
        /// Localizes and format message associated with a message key into a given locale.
        /// </summary>
        /// <param name="messageKey">The message key to find the corresponding string</param>
        /// <param name="locale">Locale in which to localize</param>
        /// <param name="objects">List of objects to insert in the format</param>
        /// <returns>The localized message if found; the message key if not</returns>
        public string LocalizeFormatString(string messageKey, string locale, params object[] objects)
        {
            var message = this.LocalizeString(messageKey, locale);
            string formattedMessage;
            try
            {
                // Try to format the string
                // Because the strings in the xml files could change, we do not want the
                // format to fail; it should only be plus value
                formattedMessage = string.Format(message, objects);
            }
            catch (Exception)
            {
                // Could not format the string
                return message;
            }

            return formattedMessage;
        }

        /// <summary>
        /// Returns all the available languages currently in cache.
        /// </summary>
        /// <returns>All available languages</returns>
        public string[] GetAvailableLanguages()
        {
            var availableLocales = new String[this.applicationMessages.Count];
            var i = 0;
            foreach (var locale in this.applicationMessages.Keys)
                availableLocales[i++] = locale;
            return availableLocales;
        }

        /// <summary>
        /// Loads application messages from application messages xml files.
        /// </summary>
        /// <param name="messageFilePaths">Paths to the message definition xml files</param>
        private void LoadApplicationMessages(IEnumerable<string> messageFilePaths)
        {
            foreach (var messageFilePath in messageFilePaths)
            {
                XDocument xmlDocument;
                try
                {
                    xmlDocument = XDocument.Load(messageFilePath);
                    if (xmlDocument.Root == null)
                    {
                        throw new XmlException();
                    }
                }
                catch (Exception exception)
                {
                    Log.Error(string.Format("File {0} could not be loaded as an xml file.", messageFilePath), exception);
                    continue;
                }

                try
                {
                    var locale = xmlDocument.Root.Attribute("language").Value;

                    IDictionary<string, string> messagesDict;
                    if (!this.applicationMessages.TryGetValue(locale, out messagesDict))
                    {
                        messagesDict = new Dictionary<string, string>();
                        this.applicationMessages.Add(locale, messagesDict);
                    }

                    var elements = xmlDocument.Root.Elements("message");
                    foreach (var element in elements)
                    {
                        var key = element.Attribute("key").Value;

                        if (messagesDict.ContainsKey(key))
                            continue;

                        var message = element.Attribute("message").Value;
                        messagesDict.Add(key, message);
                    }
                }
                catch (Exception exception)
                {
                    Log.Error(string.Format("File {0} is an invalid xml file.", messageFilePath), exception);
                }
            }
        }
    }
}