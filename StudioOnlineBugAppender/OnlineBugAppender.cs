using System;
using log4net.Appender;
using log4net.Core;

namespace StudioOnlineBugAppender
{
    public class OnlineBugAppender : AppenderSkeleton
    {
        public string VsoProject { get; set; }
        public string VsoToken { get; set; }
        public string VsoUsername { get; set; }
        public string VsoPassword { get; set; }
        public bool VsoAuthenticateWithToken { get; set; }
        public string VsoAccountName { get; set; }
        public string ProxyUrl { get; set; }
        public bool UseProxy { get; set; }
        protected override void Append(LoggingEvent loggingEvent)
        {
            this.CreateBugCreator().SendBug(this.CreateBug(loggingEvent));
        }

        private IBugCreator CreateBugCreator()
        {
            return this.VsoAuthenticateWithToken ? new BugCreator(this.VsoAccountName, this.VsoProject, this.VsoToken) { UseProxy = this.UseProxy, Proxy = new Uri(this.ProxyUrl) } 
                : new BugCreator(this.VsoAccountName, this.VsoProject, this.VsoUsername, this.VsoPassword) { UseProxy = this.UseProxy, Proxy = new Uri(this.ProxyUrl) };
        }

        private IBug CreateBug(LoggingEvent loggingEvent)
        {
            return new Bug
            {
                Creator = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                Description = RenderLoggingEvent(loggingEvent),
                Title = "[" + loggingEvent.Level + "] log from " + loggingEvent.Domain
            };
        }

        protected override bool RequiresLayout
        {
            get { return true; }
        }
    }
}
