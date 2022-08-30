namespace NortheastMegabuck.Tests.Models;

[TestFixture]
internal class ErrorDetailModelTests
{
    [Test]
    public void Constructor_Exception_FieldsMapped()
    {
        var ex = new Exception("message");

        var errorDetail = new NortheastMegabuck.Models.ErrorDetail(ex);

        Assert.Multiple(() =>
        {
            Assert.That(errorDetail.Message, Is.EqualTo(ex.Message));
            Assert.That(errorDetail.ReturnCode, Is.EqualTo(-1));
        });
    }

    [Test]
    public void Constructor_Message_FieldsMapped()
    {
        var message = "message";

        var errorDetail = new NortheastMegabuck.Models.ErrorDetail(message);

        Assert.Multiple(() =>
        {
            Assert.That(errorDetail.Message, Is.EqualTo("message"));
            Assert.That(errorDetail.ReturnCode, Is.EqualTo(-1));
        });
    }

    [Test]
    public void Constructor_MessageReturnCode_FieldsMapped()
    {
        var message = "message";
        var returnCode = 5;

        var errorDetail = new NortheastMegabuck.Models.ErrorDetail(message, returnCode);

        Assert.Multiple(() =>
        {
            Assert.That(errorDetail.Message, Is.EqualTo("message"));
            Assert.That(errorDetail.ReturnCode, Is.EqualTo(5));
        });
    }
}
