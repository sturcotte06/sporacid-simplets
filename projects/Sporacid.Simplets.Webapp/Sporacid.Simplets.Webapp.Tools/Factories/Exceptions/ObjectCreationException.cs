namespace Sporacid.Simplets.Webapp.Tools.Factories.Exceptions
{
    using System;

    public class ObjectCreationException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public ObjectCreationException()
            : this(null, null)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public ObjectCreationException(string message)
            : this(message, null)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public ObjectCreationException(Exception innerException)
            : this(null, innerException)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public ObjectCreationException(string message, Exception innerException)
            : base("The object couldn't be created. " + message ?? "", innerException)
        {
        }
    }
}
