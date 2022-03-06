namespace NewEnglandClassic.Tests
{
    [TestFixture]
    public class Ping
    {
        [Test]
        public void BusinessLogic()
        {
            var item = new PingBusiness();

            var value = item.Execute(1);

            Assert.That(value, Is.EqualTo(1));
        }

        [Test]
        public void Presentation()
        {
            var item = new PingPresentation();

            var value = item.Execute(2);

            Assert.That(value, Is.EqualTo(2));
        }

        [Test]
        public void UI()
        {
            var item = new PingUI();

            var value = item.Execute(3);

            Assert.That(value, Is.EqualTo(3));
        }
    }
}