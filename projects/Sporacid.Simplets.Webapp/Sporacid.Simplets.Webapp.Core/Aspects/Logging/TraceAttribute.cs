namespace Sporacid.Simplets.Webapp.Core.Aspects.Logging
{
    using System;
    using System.Diagnostics;
    using System.Reflection;
    using System.Text;
    using log4net;
    using PostSharp.Aspects;

    /// <summary>
    /// Aspect that, when applied on a method, emits a trace message before and
    /// after the method execution.
    /// </summary>
    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [Serializable]
    public class TraceAttribute : OnMethodBoundaryAspect
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        [NonSerialized] private readonly LoggingLevel loggingLevel;

        private string methodName;

        [NonSerialized] private TimeSpan previousTime;

        [NonSerialized] private Stopwatch stopwatch;

        public TraceAttribute(LoggingLevel loggingLevel)
        {
            this.loggingLevel = loggingLevel;
        }

        /// <summary>
        /// Method executed at build time. Initializes the aspect instance. After the execution
        /// of <see cref="CompileTimeInitialize" />, the aspect is serialized as a managed
        /// resource inside the transformed assembly, and deserialized at runtime.
        /// </summary>
        /// <param name="method">
        /// Method to which the current aspect instance
        /// has been applied.
        /// </param>
        /// <param name="aspectInfo">Unused.</param>
        public override void CompileTimeInitialize(MethodBase method, AspectInfo aspectInfo)
        {
            if (method.DeclaringType == null)
            {
                this.methodName = method.Name;
            }
            else
            {
                this.methodName = method.DeclaringType.FullName + "." + method.Name;
            }
        }

        /// <summary>
        /// Method invoked before the execution of the method to which the current
        /// aspect is applied.
        /// </summary>
        /// <param name="args">Unused.</param>
        public override void OnEntry(MethodExecutionArgs args)
        {
            if (!this.LevelEnabled())
            {
                return;
            }

            if (this.stopwatch == null)
            {
                this.stopwatch = Stopwatch.StartNew();
            }

            this.LogFormat("Entering: {0}", this.methodName);
            this.previousTime = this.stopwatch.Elapsed;
        }

        /// <summary>
        /// Method invoked after successfull execution of the method to which the current
        /// aspect is applied.
        /// </summary>
        /// <param name="args">Unused.</param>
        public override void OnSuccess(MethodExecutionArgs args)
        {
            if (!this.LevelEnabled())
            {
                return;
            }

            this.LogFormat("Exiting: {0}, Elapsed: {1}", this.methodName, this.stopwatch.Elapsed - this.previousTime);
        }

        /// <summary>
        /// Method invoked after failure of the method to which the current
        /// aspect is applied.
        /// </summary>
        /// <param name="args">Unused.</param>
        public override void OnException(MethodExecutionArgs args)
        {
            if (!this.LevelEnabled())
            {
                return;
            }

            var stringBuilder = new StringBuilder(1024);

            // Write the exit message. 
            stringBuilder.AppendFormat("Exception in {0} ", this.methodName);
            stringBuilder.Append('(');

            // Write the current instance object, unless the method 
            // is static. 
            var instance = args.Instance;
            if (instance != null)
            {
                stringBuilder.Append("this=");
                stringBuilder.Append(instance);
                if (args.Arguments.Count > 0)
                    stringBuilder.Append("; ");
            }

            // Write the list of all arguments. 
            for (var i = 0; i < args.Arguments.Count; i++)
            {
                if (i > 0)
                    stringBuilder.Append(", ");
                stringBuilder.Append(args.Arguments.GetArgument(i) ?? "null");
            }

            // Write the exception message. 
            stringBuilder.AppendFormat("): Exception ");
            stringBuilder.Append(args.Exception.GetType().Name);
            stringBuilder.Append(": ");
            stringBuilder.Append(args.Exception.Message);
            stringBuilder.Append(", ");
            stringBuilder.AppendFormat("Elapsed: {0}", this.stopwatch.Elapsed - this.previousTime);

            this.Log(stringBuilder.ToString());
        }

        private bool LevelEnabled()
        {
            switch (this.loggingLevel)
            {
                case LoggingLevel.Debug:
                    return Logger.IsDebugEnabled;
                case LoggingLevel.Information:
                    return Logger.IsInfoEnabled;
                case LoggingLevel.Warning:
                    return Logger.IsWarnEnabled;
                case LoggingLevel.Error:
                    return Logger.IsErrorEnabled;
                case LoggingLevel.Fatal:
                    return Logger.IsFatalEnabled;
            }

            return false;
        }

        /// <summary>
        /// Logs the formatted message at the configured logging level.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="formats">The format objects.</param>
        private void LogFormat(String message, params Object[] formats)
        {
            this.Log(String.Format(message, formats));
        }

        /// <summary>
        /// Logs the message at the configured logging level.
        /// </summary>
        /// <param name="message">The message</param>
        private void Log(String message)
        {
            switch (this.loggingLevel)
            {
                case LoggingLevel.Debug:
                    Logger.Debug(message);
                    break;
                case LoggingLevel.Information:
                    Logger.Info(message);
                    break;
                case LoggingLevel.Warning:
                    Logger.Warn(message);
                    break;
                case LoggingLevel.Error:
                    Logger.Error(message);
                    break;
                case LoggingLevel.Fatal:
                    Logger.Fatal(message);
                    break;
            }
        }
    }
}