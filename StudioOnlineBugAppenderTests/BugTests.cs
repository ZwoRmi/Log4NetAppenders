using NUnit.Framework;
using StudioOnlineBugAppender;

namespace StudioOnlineBugAppenderTests
{
    [TestFixture]
    public class BugTests
    {
        private Bug _target;

        [SetUp]
        public void SetUp()
        {
            _target = new Bug
            {
                Creator = "creator",
                Description = "description",
                Title = "title"
            };
        }
        [Test]
        public void DescriptionTest()
        {
            const string expected = "testDescription";
            this._target.Description = expected;
            Assert.AreEqual(expected, _target.Description);
        }

        [Test]
        public void TitleTest()
        {
            const string expected = "testTitle";
            this._target.Title = expected;
            Assert.AreEqual(expected, _target.Title);
        }

        [Test]
        public void CreatorTest()
        {
            const string expected = "testCreator";
            this._target.Creator = expected;
            Assert.AreEqual(expected, _target.Creator);
        }

        [Test]
        public void DeserializeMeTest()
        {
            const string expected = "[{\"op\":\"add\",\"path\":\"/fields/System.Title\",\"value\":\"title\"},{\"op\":\"add\",\"path\":\"/fields/System.Description\",\"value\":\"description\"},{\"op\":\"add\",\"path\":\"/fields/System.CreatedBy\",\"value\":\"createur\"}]";
            Assert.AreEqual(expected, _target.Deserialize());
        }
    }
}
