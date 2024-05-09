using KLADemo.Contracts;

namespace KLADemo.Services;

public class NumericConverter : INumericConverter
{
    public (int wholeNumber, int decimalNumber) Convert(string number)
    {
        var decimalNumber = decimal.Parse(number);

        var fracture = (decimalNumber - (int)decimalNumber) * 100;
        
        return ((int)decimalNumber, (int)fracture);
    }
}
