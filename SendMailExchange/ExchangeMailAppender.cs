using System;
using log4net.Appender;
using log4net.Core;

namespace SendMailExchange
{
    public class ExchangeMailAppender : AppenderSkeleton
    {
        public string Recipient { get; set; }
        public string MailAddressForUrl { get; set; }
        
        protected override void Append(LoggingEvent loggingEvent)
        {
            MailSender mailSender = new MailSender
            {
                Subject = "[" + loggingEvent.Level + "] log from " + loggingEvent.Domain,
                Body = Environment.NewLine + RenderLoggingEvent(loggingEvent)
            };
            if (!string.IsNullOrWhiteSpace(this.Recipient)) { mailSender.Recipient = this.Recipient; }
            if (!string.IsNullOrWhiteSpace(this.MailAddressForUrl)) { mailSender.EmailAddressForUrl= this.MailAddressForUrl; }
            mailSender.SendMail();
        }
        protected override bool RequiresLayout
        {
            get { return true; }
        }
    }
}
