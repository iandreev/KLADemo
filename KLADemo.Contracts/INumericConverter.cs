namespace KLADemo.Contracts;

public interface INumericConverter
{
    (int wholeNumber, int decimalNumber) Convert(string number);
}
