namespace Sporacid.Simplets.Webapp.Services.WebApi2.Trace
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text;
    using System.Web.Http.Tracing;
    using log4net;

    public sealed class Log4NetTraceWriter : ITraceWriter
    {
        private static readonly ILog s_log = LogManager.GetLogger(typeof (Log4NetTraceWriter));

        private static readonly Lazy<Dictionary<TraceLevel, Action<String>>> s_loggingMap =
            new Lazy<Dictionary<TraceLevel, Action<String>>>(() => new Dictionary<TraceLevel, Action<String>>
            {
                {TraceLevel.Info, s_log.Info},
                {TraceLevel.Debug, s_log.Debug},
                {TraceLevel.Error, s_log.Error},
                {TraceLevel.Fatal, s_log.Fatal},
                {TraceLevel.Warn, s_log.Warn}
            });

        private Dictionary<TraceLevel, Action<String>> Logger
        {
            get { return s_loggingMap.Value; }
        }

        public void Trace(HttpRequestMessage request, String category, TraceLevel level, Action<TraceRecord> traceAction)
        {
            if (level == TraceLevel.Off)
                return;

            var record = new TraceRecord(request, category, level);
            traceAction(record);
            this.Log(record);
        }

        private void Log(TraceRecord record)
        {
            var message = new StringBuilder();

            if (record.Request != null)
            {
                if (record.Request.Method != null)
                    message.Append(" ").Append(record.Request.Method.Method);

                if (record.Request.RequestUri != null)
                    message.Append(" ").Append(record.Request.RequestUri.AbsoluteUri);
            }

            if (!String.IsNullOrWhiteSpace(record.Category))
                message.Append(" ").Append(record.Category);

            if (!String.IsNullOrWhiteSpace(record.Operator))
                message.Append(" ").Append(record.Operator).Append(" ").Append(record.Operation);

            if (!String.IsNullOrWhiteSpace(record.Message))
                message.Append(" ").Append(record.Message);

            if (record.Exception != null && !String.IsNullOrEmpty(record.Exception.GetBaseException().Message))
                message.Append(" ").AppendLine(record.Exception.GetBaseException().Message);

            this.Logger[record.Level](message.ToString());
        }
    }
}