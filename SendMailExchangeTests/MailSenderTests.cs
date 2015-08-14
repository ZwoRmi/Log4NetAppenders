using System;
using NUnit.Framework;
using SendMailExchange;

namespace SendMailExchangeTests
{
    [TestFixture]
    public class MailSenderTests
    {
        private MailSender _target;

        [SetUp]
        public void SetUp()
        {
            this._target = new MailSender();
        }

        [Test]
        public void BodyTest()
        {
            string expected = "testBody" + Environment.NewLine + "test";
            this._target.Body = expected;
            Assert.AreEqual(expected.Replace(Environment.NewLine,"<br/>"), _target.Body);
        }

        [Test]
        public void RecipientTest()
        {
            const string expected = "testRecipient";
            this._target.Recipient = expected;
            Assert.AreEqual(expected, _target.Recipient);
        }

        [Test]
        public void SubjectTest()
        {
            const string expected = "testSubject";
            this._target.Subject = expected;
            Assert.AreEqual(expected, _target.Subject);
        }

        [Test]
        public void EmailAddressForUrlTest()
        {
            const string expected = "testEmailAddressForUrl";
            this._target.EmailAddressForUrl = expected;
            Assert.AreEqual(expected, _target.EmailAddressForUrl);
        }

        [Ignore] //This test send a mail, we don't want to fill the email box of the recipient..
        [Test]  //And there is no constraint, we just check that there is no exception
        public void SendMailTest()
        {
            _target.Body = "test" + Environment.NewLine + "test";
            _target.SendMail();
        }
    }
}
