using KLADemo.Contracts;
using System.Globalization;
using System.Text.RegularExpressions;

namespace KLADemo.Services;

public class NumericValidator : INumericValidator
{
    private readonly NumberFormatInfo _numberFormat;
    public NumericValidator()
    {
        _numberFormat = new NumberFormatInfo
        {
            NumberDecimalSeparator = INumericService.DECIMAL_SEPARATOR,
            NumberGroupSeparator = " ",
        };
    }

    public void Validate(string number)
    {
        ValidateDecimalPlaces(number);
        ValidateIsNumber(number);
        ValidateScale(number);
        ValidatePositive(number);
        ValidateMax(number);
        ValidateFormat(number);
    }

    private void ValidateDecimalPlaces(string number)
    {
        if (number.Contains(INumericService.DECIMAL_SEPARATOR))
        {
            var numberParts = number.Split(INumericService.DECIMAL_SEPARATOR,
                StringSplitOptions.RemoveEmptyEntries);
            if (numberParts.Length != 2)
            {
                throw new ArgumentException("input should be a number", nameof(number));
            }

            if (numberParts[0].EndsWith(" "))
            {
                throw new ArgumentException("input should be a number", nameof(number));
            }

            if (numberParts[1].StartsWith(" "))
            {
                throw new ArgumentException("input should be a number", nameof(number));
            }
        }
    }

    private void ValidateIsNumber(string number)
    {
        if (!decimal.TryParse(number, _numberFormat, out var money))
        {
            throw new ArgumentException("input should be a number", nameof(number));
        }
    }

    private void ValidateScale(string number)
    {
        var money = decimal.Parse(number, _numberFormat);
        if (money.Scale > 2)
        {
            throw new ArgumentException("number of cents should be between 0 and 99", nameof(number));
        }
    }

    private void ValidatePositive(string number)
    {
        if (number.Contains("-"))
        {
            throw new ArgumentException("number should be positive", nameof(number));
        }
    }

    private void ValidateMax(string number)
    {
        var money = decimal.Parse(number, _numberFormat);
        if ((int)money > 999_999_999)
        {
            throw new ArgumentException("number should be less or equal to 999 999 999", nameof(number));
        }
    }

    private void ValidateFormat(string number)
    {
        var regex = @"^([0-9]|[1-9][0-9]|[1-9][0-9][0-9]|[1-9][0-9][0-9][0-9]|[1-9] [0-9][0-9][0-9]|[1-9][0-9][0-9][0-9][0-9]|[1-9][0-9] [0-9][0-9][0-9]|[1-9][0-9][0-9][0-9][0-9][0-9]|[1-9][0-9][0-9] [0-9][0-9][0-9]|[1-9][0-9][0-9][0-9][0-9][0-9][0-9]|[1-9][0-9][0-9][0-9] [0-9][0-9][0-9]|[1-9] [0-9][0-9][0-9] [0-9][0-9][0-9]|[1-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]|[1-9][0-9][0-9][0-9][0-9] [0-9][0-9][0-9]|[1-9][0-9] [0-9][0-9][0-9] [0-9][0-9][0-9]|[1-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]|[1-9][0-9][0-9][0-9][0-9][0-9] [0-9][0-9][0-9]|[1-9][0-9][0-9] [0-9][0-9][0-9] [0-9][0-9][0-9])$";

        if (number.Contains(INumericService.DECIMAL_SEPARATOR))
        {
            number = number.Split(INumericService.DECIMAL_SEPARATOR)[0];
        }

        if (!Regex.IsMatch(number, regex))
        {
            throw new ArgumentException("number should be in the correct format", nameof(number));
        }
    }
}
