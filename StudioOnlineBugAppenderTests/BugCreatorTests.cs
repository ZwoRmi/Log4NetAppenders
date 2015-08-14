using NUnit.Framework;
using StudioOnlineBugAppender;

namespace StudioOnlineBugAppenderTests
{
    [TestFixture]
    public class BugCreatorTests
    {
        [Ignore] //this code might not be called because it will fill real database
        [Test]
        public void CreateBugTest()
        {
            Bug bug = new Bug
            {
                Creator = "creator",
                Description = "description",
                Title = "Test"
            };
            //Set your informations there
            BugCreator bc = new BugCreator("ACCOUNT", "PROJECT", "TOKEN");
            bc.SendBug(bug);
            Assert.That(bc.Response.ReadAsStringAsync().Result.Substring(0,6), Is.EqualTo("{\"id\":"));
        }
    }
}
