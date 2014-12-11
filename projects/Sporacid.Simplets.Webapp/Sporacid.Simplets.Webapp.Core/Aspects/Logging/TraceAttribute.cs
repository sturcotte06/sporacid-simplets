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

        private string methodName;

        [NonSerialized] private TimeSpan previousTime;

        [NonSerialized] private Stopwatch stopwatch;

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
            if (this.stopwatch == null)
            {
                this.stopwatch = Stopwatch.StartNew();
            }

            Logger.DebugFormat("Entering: {0}", this.methodName);
            this.previousTime = this.stopwatch.Elapsed;
        }

        /// <summary>
        /// Method invoked after successfull execution of the method to which the current
        /// aspect is applied.
        /// </summary>
        /// <param name="args">Unused.</param>
        public override void OnSuccess(MethodExecutionArgs args)
        {
            Logger.DebugFormat("Exiting: {0}, Elapsed: {1}", this.methodName, this.stopwatch.Elapsed - this.previousTime);
        }

        /// <summary>
        /// Method invoked after failure of the method to which the current
        /// aspect is applied.
        /// </summary>
        /// <param name="args">Unused.</param>
        public override void OnException(MethodExecutionArgs args)
        {
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

            Logger.Warn(stringBuilder);
        }
    }
}