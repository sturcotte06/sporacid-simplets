namespace Sporacid.Simplets.Webapp.Core.Localization
{
    /// <summary>
    ///     Interface for all localizable objects.
    /// </summary>
    /// <author>Simon Turcotte-Langevin</author>
    public interface ILocalizable
    {
        /// <summary>
        ///     Localizes an object into the default locale.
        /// </summary>
        /// <param name="localizer">An IMessageLocalizer implementation instance</param>
        void Localize(IMessageLocalizer localizer);

        /// <summary>
        ///     Localizes an object into the given locale.
        /// </summary>
        /// <param name="localizer">An IMessageLocalizer implementation instance</param>
        /// <param name="locale">The locale in which to localize</param>
        void Localize(IMessageLocalizer localizer, string locale);
    }
}