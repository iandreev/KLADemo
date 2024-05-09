using FluentAssertions;
using KLADemo.Services;

namespace KLADemo.Tests;

public class NumericServiceTests
{
    [TestCase("0", "zero dollars")]
    [TestCase("1", "one dollar")]
    [TestCase("25,1", "twenty-five dollars and ten cents")]
    [TestCase("0,01", "zero dollars and one cent")]
    [TestCase("45 100", "forty-five thousand one hundred dollars")]
    [TestCase("999 999 999,99", "nine hundred ninety-nine million nine hundred ninety-nine thousand nine hundred ninety-nine dollars and ninety-nine cents")]
    public void Should_Return_Correct_Result(string input, string expected)
    {
        var service = new NumericService(new NumericConverter(), new NumericValidator());

        var result = service.ConvertToStringRepresentation(input);
        result.Should().Be(expected);
    }
}