using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using NortheastMegabuck.Tests.Extensions;

namespace NortheastMegabuck.Tests.Bowlers.Update;

[TestFixture]
internal class BusinessLogic
{
    private Mock<IValidator<NortheastMegabuck.Models.PersonName>> _nameValidator;
    private Mock<NortheastMegabuck.Bowlers.Update.IDataLayer> _dataLayer;

    private NortheastMegabuck.Bowlers.Update.IBusinessLogic _businessLogic;

    [SetUp]
    public void SetUp()
    {
        _nameValidator = new Mock<IValidator<NortheastMegabuck.Models.PersonName>>();
        _dataLayer = new Mock<NortheastMegabuck.Bowlers.Update.IDataLayer>();

        _businessLogic = new NortheastMegabuck.Bowlers.Update.BusinessLogic(_nameValidator.Object, _dataLayer.Object);
    }

    [Test]
    public void Execute_BowlerIdPersonName_NameValidatorValidate_CalledCorrectly()
    {
        _nameValidator.Validate_IsValid();

        var bowlerId = BowlerId.New();
        var name = new NortheastMegabuck.Models.PersonName();

        _businessLogic.Execute(bowlerId, name);

        _nameValidator.Verify(validator=> validator.Validate(name), Times.Once);
    }

    [Test]
    public void Execute_BowlerIdPersonName_NameValidatorValidateFalse_ErrorFlow()
    {
        _nameValidator.Validate_IsNotValid("errorMessage");

        var bowlerId = BowlerId.New();
        var name = new NortheastMegabuck.Models.PersonName();

        _businessLogic.Execute(bowlerId, name);

        Assert.Multiple(() =>
        {
            _businessLogic.Errors.Assert_HasErrorMessage("errorMessage");

            _dataLayer.Verify(dataLayer => dataLayer.Execute(It.IsAny<BowlerId>(), It.IsAny<NortheastMegabuck.Models.PersonName>()), Times.Never);
        });
    }

    [Test]
    public void Execute_BowlerIdPersonName_NameValidatorValidateTrue_DataLayerExecute_CalledCorrectly()
    {
        _nameValidator.Validate_IsValid();

        var bowlerId = BowlerId.New();
        var name = new NortheastMegabuck.Models.PersonName();

        _businessLogic.Execute(bowlerId, name);

        _dataLayer.Verify(dataLayer => dataLayer.Execute(bowlerId, name), Times.Once);
    }

    [Test]
    public void Execute_BowlerIdPersonName_NameValidatorValidateTrue_DataLayerExecuteThrowsException_ExceptionFlow()
    {
        _nameValidator.Validate_IsValid();

        var ex = new Exception("exception");
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<BowlerId>(), It.IsAny<NortheastMegabuck.Models.PersonName>())).Throws(ex);

        var bowlerId = BowlerId.New();
        var name = new NortheastMegabuck.Models.PersonName();

        _businessLogic.Execute(bowlerId, name);

        _businessLogic.Errors.Assert_HasErrorMessage("exception");
    }
}
