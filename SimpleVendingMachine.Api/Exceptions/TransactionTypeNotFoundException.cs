namespace SimpleVendingMachine.Api.Exceptions
{
    public class TransactionTypeNotFoundException : Exception
    {
        public TransactionTypeNotFoundException() { }

        public TransactionTypeNotFoundException(string message) : base(message) { }

        public TransactionTypeNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}
