

namespace SimpleECommerce.Core.Exceptions
{
    public class DuplicateProductException : ArgumentException
    {
        public DuplicateProductException(string message) : base(message)
        {
        }
        public DuplicateProductException(string message, Exception innerException) : base(message, innerException)
        {
        }
        public DuplicateProductException() : base("A product with the same identifier already exists.")
        {
        }

    }
}

