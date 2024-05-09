using FluentAssertions;
using KLADemo.Contracts;
using KLADemo.Services;
using NSubstitute;

namespace KLADemo.Tests;

public class NumericServiceUnit
{

    [Test]
    public void Should_Validate()
    {
        var validator = Substitute.For<INumericValidator>();
        var converter = Substitute.For<INumericConverter>();

        var service = new NumericService(converter, validator);

        service.ConvertToStringRepresentation("123");
        validator.Received().Validate("123");
    }

    [Test]
    public void Should_Convert()
    {
        var validator = Substitute.For<INumericValidator>();
        var converter = Substitute.For<INumericConverter>();

        var service = new NumericService(converter, validator);

        service.ConvertToStringRepresentation("123");
        converter.Received().Convert("123");
    }

}