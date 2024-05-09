namespace KLADemo.Contracts;

public interface INumericService
{
    const string DECIMAL_SEPARATOR = ",";

    string ConvertToStringRepresentation(string number);
}
