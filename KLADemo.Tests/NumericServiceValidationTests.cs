using FluentAssertions;
using KLADemo.Services;

namespace KLADemo.Tests;

public class NumericServiceValidationTests
{

    [Test]
    public void Should_Throw_When_Empty()
    {
        var service = new NumericService(new NumericConverter(), new NumericValidator());

        var action = () => service.ConvertToStringRepresentation("");
        action.Should().Throw<ArgumentException>().WithMessage("input should be a number (Parameter 'number')");
    }

    [Test]
    public void Should_Throw_When_NoN()
    {
        var service = new NumericService(new NumericConverter(), new NumericValidator());

        var action = () => service.ConvertToStringRepresentation("sadfgsdfg");
        action.Should().Throw<ArgumentException>().WithMessage("input should be a number (Parameter 'number')");
    }

    [TestCase("123,")]
    [TestCase(",22")]
    [TestCase("123 ,22")]
    [TestCase("123 ,33")]
    [TestCase("123.33")]
    public void Should_Throw_When_Wrong_Formatting(string input)
    {
        var service = new NumericService(new NumericConverter(), new NumericValidator());

        var action = () => service.ConvertToStringRepresentation(input);
        action.Should().Throw<ArgumentException>().WithMessage("input should be a number (Parameter 'number')");
    }

    [TestCase("12 3")]
    [TestCase("123 22")]
    [TestCase("12 3 22")]
    [TestCase("1 23 22")]
    [TestCase("1 2 2")]
    public void Should_Throw_When_Wrong_Formatting_Spaces(string input)
    {
        var service = new NumericService(new NumericConverter(), new NumericValidator());

        var action = () => service.ConvertToStringRepresentation(input);
        action.Should().Throw<ArgumentException>().WithMessage("number should be in the correct format (Parameter 'number')");
    }

    [Test]
    public void Should_Throw_When_TooMany_Cents()
    {
        var service = new NumericService(new NumericConverter(), new NumericValidator());

        var action = () => service.ConvertToStringRepresentation("123,100");
        action.Should().Throw<ArgumentException>().WithMessage("number of cents should be between 0 and 99 (Parameter 'number')");
    }

    [Test]
    public void Should_Throw_When_Negative()
    {
        var service = new NumericService(new NumericConverter(), new NumericValidator());

        var action = () => service.ConvertToStringRepresentation("-123");
        action.Should().Throw<ArgumentException>().WithMessage("number should be positive (Parameter 'number')");
    }

    [Test]
    public void Should_Throw_When_Too_Big()
    {
        var service = new NumericService(new NumericConverter(), new NumericValidator());

        var action = () => service.ConvertToStringRepresentation("1 000 000 000");
        action.Should().Throw<ArgumentException>().WithMessage("number should be less or equal to 999 999 999 (Parameter 'number')");
    }
}