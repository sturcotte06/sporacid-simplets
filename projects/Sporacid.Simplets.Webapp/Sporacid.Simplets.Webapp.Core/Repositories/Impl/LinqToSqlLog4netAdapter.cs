namespace Sporacid.Simplets.Webapp.Core.Repositories.Impl
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using log4net;
    using log4net.Core;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class LinqToSqlLog4netAdapter : TextWriter
    {
        private readonly Level level;
        private readonly Dictionary<Level, Action<string>> loggingMap;

        public LinqToSqlLog4netAdapter(ILog logger, Level level)
        {
            this.level = level;
            this.loggingMap = new Dictionary<Level, Action<string>>
            {
                {Level.Info, logger.Info},
                {Level.Debug, logger.Debug},
                {Level.Error, logger.Error},
                {Level.Fatal, logger.Fatal},
                {Level.Warn, logger.Warn}
            };
        }

        /// <summary>
        /// When overridden in a derived class, returns the character encoding in which the output is written.
        /// </summary>
        /// <returns>
        /// The character encoding in which the output is written.
        /// </returns>
        public override Encoding Encoding
        {
            get { return Encoding.UTF8; }
        }

        /// <summary>
        /// Writes a string to the text string or stream.
        /// </summary>
        /// <param name="value">The string to write. </param>
        /// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
        public override void Write(string value)
        {
            loggingMap[this.level](value);
        }

        public override void Write(char[] buffer, int index, int count)
        {
            this.Write(new string(buffer, index, count));
        }
    }
}