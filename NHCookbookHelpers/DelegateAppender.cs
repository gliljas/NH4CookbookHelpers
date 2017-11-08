using System;
using log4net.Appender;
using log4net.Core;

namespace NH4CookbookHelpers
{
    public class DelegateAppender : AppenderSkeleton
    {
        private readonly Action<string> _action;

        public DelegateAppender(Action<string> action)
        {
            _action = action;
        }

        protected override void Append(LoggingEvent loggingEvent)
        {
            _action(RenderLoggingEvent(loggingEvent).Replace("\n",Environment.NewLine));
        }
    }
}