
namespace RetailInMotion.Domain.Exceptions
{
    public class InvalidPostCodeException : Exception
    {
        public InvalidPostCodeException(string postCode)
            : base($"Post Code \"{postCode}\" is invalid.")
        {
        }
    }
}