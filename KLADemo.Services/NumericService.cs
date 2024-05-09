using KLADemo.Contracts;
using System.Data.SqlTypes;
using System.Text;

namespace KLADemo.Services;

public class NumericService : INumericService
{
    private readonly INumericConverter _converter;
    private readonly INumericValidator _validator;

    public NumericService(INumericConverter converter, INumericValidator validator)
    {
        _converter = converter;
        _validator = validator;
    }

    public string ConvertToStringRepresentation(string number)
    {
        _validator.Validate(number);
        var (wholeNumber, decimalNumber) = _converter.Convert(number);

        return ConvertWholePart(wholeNumber) + ConvertDecimalPart(decimalNumber);
    }

    private string ConvertWholePart(int wholeNumber)
    {
        if (wholeNumber == 0)
        {
            return $"zero dollars";
        }

        StringBuilder sb = new();
        int lowerPart = wholeNumber % 1000;
        sb.Append($"{ConvertNumberPart(lowerPart)} dollar{(IsSingle(lowerPart) ? "" : "s")}");

        wholeNumber = (wholeNumber - lowerPart) / 1000;
        lowerPart = wholeNumber % 1000;
        if (lowerPart > 0)
        {
            sb.Insert(0, $"{ConvertNumberPart(lowerPart)} thousand ");
        }

        wholeNumber = (wholeNumber - lowerPart) / 1000;
        lowerPart = wholeNumber % 1000;
        if (lowerPart > 0)
        {
            sb.Insert(0, $"{ConvertNumberPart(lowerPart)} million ");
        }

        return sb.ToString();
    }

    private string ConvertDecimalPart(int decimalNumber) =>
        decimalNumber > 0
        ? $" and {ConvertNumberPart(decimalNumber)} cent{(IsSingle(decimalNumber) ? "" : "s")}"
        : string.Empty;

    private bool IsSingle(int number) => number % 10 == 1;

    private string ConvertNumberPart(int number)
    {
        var map = new Dictionary<int, string>
        {
            {1, "one"},
            {2, "two"},
            {3, "three"},
            {4, "four"},
            {5, "five"},
            {6, "six"},
            {7, "seven"},
            {8, "eight"},
            {9, "nine"},
        };
        var mapTeens = new Dictionary<int, string>
        {
            {10, "ten"},
            {11, "eleven"},
            {12, "twelve"},
            {13, "thirteen"},
            {14, "fourteen"},
            {15, "fifteen"},
            {16, "sixteen"},
            {17, "seventeen"},
            {18, "eighteen"},
            {19, "nineteen"},
        };
        var mapDecimals = new Dictionary<int, string>
        {
            {2, "twenty"},
            {3, "thirty"},
            {4, "forty"},
            {5, "fifty"},
            {6, "sixty"},
            {7, "seventy"},
            {8, "eighty"},
            {9, "ninety"},
        };

        StringBuilder sb = new();
        if (number / 100 != 0)
        {
            sb.Append($"{map[number / 100]} hundred");
            if (number % 100 != 0)
            {
                sb.Append(" ");
            }
        }

        var decimals = number % 100;
        if (decimals == 0)
        {
            return sb.ToString();
        }

        if (decimals < 10)
        {
            sb.Append($"{map[decimals]}");
        }
        else if (decimals <= 19)
        {
            sb.Append($"{mapTeens[decimals]}");
        }
        else
        {
            var decimalHigh = decimals / 10;
            var decimalLow = decimals % 10;

            sb.Append($"{mapDecimals[decimalHigh]}{(decimalLow > 0 ? $"-{map[decimalLow]}" : "")}");
        }

        return sb.ToString();
    }
}
