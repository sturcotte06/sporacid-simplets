namespace Sporacid.Simplets.Webapp.Tools.Threading
{
    using System;
    using System.Globalization;
    using System.Threading;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public static class ThreadingExtensions
    {
        /// <summary>
        /// Sets a thread in the given culture.
        /// </summary>
        /// <param name="thread">The thread to localize.</param>
        /// <param name="culture">The culture, as a string. Either xx or XX-xx.</param>
        public static void ToCulture(this Thread thread, String culture)
        {
            var cultureInfo = CultureInfo.CreateSpecificCulture(culture);
            thread.ToCulture(cultureInfo);
        }

        /// <summary>
        /// Sets a thread in the given culture.
        /// </summary>
        /// <param name="thread">The thread to localize.</param>
        /// <param name="culture">The culture object.</param>
        public static void ToCulture(this Thread thread, CultureInfo culture)
        {
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }
    }
}