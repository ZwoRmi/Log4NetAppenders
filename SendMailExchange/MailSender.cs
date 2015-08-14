using System.Net;
using Microsoft.Exchange.WebServices.Data;
using SendMailExchange.Extensions;
using SendMailExchange.Properties;

namespace SendMailExchange
{
    public class MailSender
    {
        private string _body;
        public string Recipient { get; set; }
        public string Subject { get; set; }
        public string Body
        {
            get { return this._body; }
            set { this._body = value.ToHtmlNewLines(); } //Format the value to have NewLines displayed in mail body
        }
        public string EmailAddressForUrl { get; set; }

        public MailSender()
        {
            this.Recipient = Settings.Default.defaultRecipient;
            this.Body = "Body";
            this.Subject = "Subject";
            this.EmailAddressForUrl = Settings.Default.defaultRecipient;
        }

        public void SendMail()
        {
            ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2007_SP1);
            service.AutodiscoverUrl(this.EmailAddressForUrl);
            service.Credentials = CredentialCache.DefaultNetworkCredentials;
            EmailMessage message = new EmailMessage(service)
            {
                Subject = this.Subject,
                Body = new MessageBody(BodyType.HTML, this.Body)
            };
            message.ToRecipients.Add(this.Recipient);
            message.Send();
        }
    }
}
